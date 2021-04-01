# Song Chords Recognizer - Web ASP.NET Application

## Recognizer Controller

#### ***/index***

Default index page where you can upload and process an audio file with any of provided models.

- Method: GET
- Arguments: None

![index prtsc](./docs/imgs/index.png)


#### ***/VisualizeStatisticalModel***
Outcome of [Song Chords Recognizer](../ACR_Pipeline/ReadMe.md) model based on Deep Learning with its key and bpm.

- Method: POST
- Arguments: IFormFile audio

![VisualizeStatisticalModel prtsc](./docs/imgs/VisualizeStatisticalModel.png)


#### ***/VisualizeTemplateVoter***
Outcome of [Song Chords Recognizer](./SongChordsRecognizer/ReadMe.md) model based on the simple template voting from generated and filtered spectrograms.

- Method: POST
- Arguments: IFormFile audio, String windowArg, String filtrationArg, int sampleLengthLevel, int bpm

![VisualizeTemplateVoter prtsc](./docs/imgs/VisualizeTemplateVoter.png)


#### ***/About***
Basic information about the project with the GitHub link.

- Method: GET
- Arguments: None

![About prtsc](./docs/imgs/About.png)


#### */IncorrectInputFormat*
An error message when some error occures.

- Method: GET
- Arguments: String messages

![IncorrectInputFormat prtsc](./docs/imgs/IncorrectInputFormat.png)
