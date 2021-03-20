#!/usr/bin/env python3
import argparse
import numpy as np
from ACR_Training.Models import MLP_scalered
from ACR_Training.Datasets import IsophonicsDataset, Dataset
from KeyRecognizer import KeyRecognizer
from DataPreprocessor import DataPreprocessor
from ChordVoter import ChordVoter

parser = argparse.ArgumentParser()
# Song Chords Recognizer arguments
parser.add_argument("--waveform", default=[], type=list, help="")
parser.add_argument("--sample_rate", default=44100, type=int, help="")

# Training args
parser.add_argument("--seed", default=42, type=int, help="Random seed.")



def main(args):
    # Arguments
    hop_length = 512
    window_size = 5
    waveform = args.waveform
    sample_rate = args.sample_rate


    # Load models
    basic_mlp = MLP_scalered.load('./models/basic_mlp.model')
    C_transposed_mlp = MLP_scalered.load('./models/C_transposed_mlp.model')



    # Preprocess Data
    x = DataPreprocessor.flatten_preprocess(waveform=waveform, sample_rate=sample_rate, hop_length=hop_length, window_size=window_size)

    # Get list of played chords
    baisc_chord_prediction = basic_mlp.predict(x)
    chords, counts = np.unique(baisc_chord_prediction, return_counts=True)
    chord_counts = dict(zip(chords, counts))

    # Get song's key (not really tonic, A moll is same as a C major or D dorian)
    key = KeyRecognizer.estimate_key(chord_counts)

    # Tranapose Song to a C major
    x_transposed = DataPreprocessor.flatten_transpose_preprocess(waveform=waveform, sample_rate=sample_rate, hop_length=hop_length, window_size=window_size, key=key)

    # Get chord sequence of a song
    transposed_chord_prediction = C_transposed_mlp.predict(x_transposed)

    # Chord voting for each beat
    chord_sequence = ChordVoter.vote_for_beats(chord_sequence=transposed_chord_prediction, waveform=waveform, sample_rate=sample_rate)

    return chord_sequence

if __name__ == "__main__":
    args = parser.parse_args([] if "__file__" not in globals() else None)

    main(args)