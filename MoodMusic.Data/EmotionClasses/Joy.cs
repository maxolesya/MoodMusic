using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    public  class Joy : IEmotion
    {
        public bool Check(EmotionsTypes e)
        {
            return e.Happiness > 0.48 && e.Surprise > 0.1 && e.Neutral > 0.08;
        }

        public Emotions GetEmotion()
        {
            return Emotions.Joy;
        }
    }
}
