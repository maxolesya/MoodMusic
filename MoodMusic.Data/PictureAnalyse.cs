using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using System.IO;
using MoodMusic.Data.EmotionClasses;


namespace MoodMusic.Data
{
    public class PictureAnalyse
    {
        public static string APIKey = "622793716f4a438bb6bad8b3aef90088";
        public static string APIKeyPrimary = "f35f0d5041254deba1bfc318e0a6b942";
        public static EmotionServiceClient Cli = new EmotionServiceClient(APIKey);
       static List<IEmotion> checkList = new List<IEmotion> {
            new Alarm(),
            new Anger(),
            new Contempt(),
            new Disgust(),
            new Disgust(),
            new Fear(),
            new Happiness(),
            new Joy(),
        new Neutral(),
        new Sadness(),
        new Suprise(),
        new Default()
        };
        public static async Task<EmotionsTypes> Compare(Stream stream)
        {
            var r1 = await Cli.RecognizeAsync(stream);          
            if (r1 != null && r1.Length > 0)
            {
                var e1 = r1[0].Scores;
                
                return new EmotionsTypes()
                {
                    Happiness = e1.Happiness,
                    Anger = e1.Anger,
                    Sadness = e1.Sadness,
                    Surprise = e1.Surprise,
                    Neutral = e1.Neutral,
                    Fear = e1.Fear,
                    Disgust = e1.Disgust,
                    Contempt = e1.Contempt
                };
                //1) Больше 95 процентов

            }
            else
                return null;
        }
        public static Emotions GetEmotion(EmotionsTypes e)
        {        
            return  (checkList.First(e1=>e1.Check(e))).GetEmotion();           
        }
    }
}
