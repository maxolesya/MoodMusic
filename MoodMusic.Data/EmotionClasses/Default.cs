using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    public  class Default : IEmotion
    {
        List<float> emotionList;
        public bool Check(EmotionsTypes e)
        {
            emotionList = new List<float> { e.Happiness, e.Anger, e.Sadness,e.Surprise,e.Neutral,
            e.Fear,e.Disgust,e.Contempt};
            return true;
        }

        public Emotions GetEmotion()
        {
            return (Emotions)emotionList.IndexOf(emotionList.Max());
        }
    }
}
