using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    public enum Emotions
    {
        Happiness,     
        Anger,
        Sadness,
        Surprise,
        Neutral,
        Fear,
        Disgust,
        Contempt,
        Joy,    
        Alarm,
        Depression,     
        Confidence  
      
    }
    interface IEmotion
    {
       bool Check(EmotionsTypes e);
       Emotions GetEmotion();

    }
}
