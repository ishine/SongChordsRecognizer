﻿using SongChordsRecognizer.MusicFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSongChordsRecognizer.Models
{
    public class PredictorsResponse
    {
        #region Properties

        /// <summary>
        /// Sequence of chords played in an audio.
        /// </summary>
        public List<Chord> ChordSequence;

        /// <summary>
        /// Times of beats in ChordSequence list.
        /// </summary>
        public List<double> BeatTimes;

        /// <summary>
        /// Key in Ionian form of the song.
        /// </summary>
        public String Key;

        /// <summary>
        /// Beats per minute value.
        /// </summary>
        public String BPM;

        /// <summary>
        /// Number of quarters in one bar.
        /// </summary>
        /// <returns></returns>
        public int BarQuarters;


        #endregion


        #region ToString

        /// <summary>
        /// Convert ChordSequence List to justified and readable string.
        /// </summary>
        /// <returns>String of song's chord sequence.</returns>
        public override string ToString()
        {
            String result = "";
            String newLine = Environment.NewLine;
            if (ChordSequence != null)
            {
                // ----------------- PRINT CHORDS -----------------
                result += newLine;
                result += new String('-', 56) + " CHORDS " + new String('-', 56) + newLine;
                result += newLine;
                for (int i = 0; i < ChordSequence.Count; i++)
                {
                    result += ChordSequence[i].Description.PadRight(10);
                    if ((i + 1) % 12 == 0) result += newLine;
                }
                result += newLine;
                result += new String('-', 56) + " CHORDS " + new String('-', 56) + newLine;
                result += newLine;
                // -------------------------------------------------
            }
            else
            {
                result = "There is no processed audio.";
            }
            return result;
        }



        #endregion
    }
}