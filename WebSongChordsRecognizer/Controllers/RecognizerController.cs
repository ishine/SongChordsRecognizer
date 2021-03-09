﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SongChordsRecognizer.ErrorMessages;
using SongChordsRecognizer.FourierTransform;
using SongChordsRecognizer.Graphs;
using SongChordsRecognizer.Parsers;
using System;
using System.Diagnostics;
using WebSongChordsRecognizer.Models;

namespace WebSongChordsRecognizer.Controllers
{
    /// <summary>
    /// ASP.NET Controller serving the SongChordRecognizer application.
    /// </summary>
    public class RecognizerController : Controller
    {
        #region Fields

        private readonly ILogger<RecognizerController> _logger;



        #endregion


        #region Initialization

        public RecognizerController(ILogger<RecognizerController> logger)
        {
            _logger = logger;
        }



        #endregion


        #region Endpoints

        /// <summary>
        /// The default page. 
        /// There is a form to upload and process any WAV audio.
        /// </summary>
        /// <returns>IActionResult, HTML View of a form.</returns>
        public IActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// The visualization page.
        /// There is a chord sequence that was generated from uploaded audio file.
        /// It can be runned from Recognizer's Index page via upload form.
        /// </summary>
        /// <param name="audio">IFormFile audio file in WAV format.</param>
        /// <param name="windowArg">String argument for one of the provided convolutional windows.</param>
        /// <param name="filtrationArg">String argument for one of the provided spectrogram filtrations.</param>
        /// <param name="sampleLengthLevel">Int argument of logarithm of fourier transform length.</param>
        /// <param name="bpm">Int argument of beats per minute</param>
        /// <returns>IActionResult, HTML View of a chord sequence, or error page.</returns>
        [HttpPost]
        public IActionResult VisualizeChordSequence(IFormFile audio, String windowArg, String filtrationArg, int sampleLengthLevel, int bpm)
        {
            IWindow window;
            ISpectrogramFiltration filtration;
            RecognizerModel model = new RecognizerModel();

            // Handle exceptions on input
            try
            {
                if (audio == null)
                    throw new Exception(ErrorMessages.RecognizerController_MissingAudio);
                if (!(bpm >= 5 && bpm <= 350))
                    throw new Exception(ErrorMessages.RecognizerController_InvalidSampleLengthLevel);
                if (!(sampleLengthLevel >= 10 && sampleLengthLevel <= 18))
                    throw new Exception(ErrorMessages.RecognizerController_InvalidSampleLengthLevel);

                window = InputArgsParser.ParseSTFTWindow(windowArg);
                filtration = InputArgsParser.ParseFiltration(filtrationArg);

                // process audio, generate chord sequence
                model.ProcessAudio(audio, window, filtration, sampleLengthLevel, bpm);
            }
            catch (Exception e)
            {
                return RedirectToAction("IncorrectInputFormat", new { message = e.Message });
            }


            return View(model);
        }



        /// <summary>
        /// The error page.
        /// When error has occured, this page will be showed with appropriate error message.
        /// </summary>
        /// <param name="message">Error message to print.</param>
        /// <returns>IActionResult, HTML View of error message.</returns>
        [HttpGet]
        public IActionResult IncorrectInputFormat(String message)
        {
            return View(new ErrorMessageModel { Message = message });
        }



        /// <summary>
        /// The About Project page.
        /// Informations about the Song Chords Recognizer project.
        /// </summary>
        /// <returns>IActionResult, HTML View with info.</returns>
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        #endregion
    }
}