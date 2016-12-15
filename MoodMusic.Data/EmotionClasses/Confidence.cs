using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    class Confidence : IEmotion
    {
        public bool Check(EmotionsTypes e)
        {
            return e.Contempt > 0.2 && e.Disgust > 0.002 && e.Neutral > 0.1;
        }

        public Emotions GetEmotion()
        {
            return Emotions.Confidence;
        }
    }
}
