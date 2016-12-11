using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    public class Neutral : IEmotion
    {
        public bool Check(EmotionsTypes e)
        {
            return e.Neutral > 0.9;
        }

        public Emotions GetEmotion()
        {
            return Emotions.Neutral;
        }
    }
}
