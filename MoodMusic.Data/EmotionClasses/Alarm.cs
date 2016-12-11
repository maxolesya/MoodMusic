using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
   public class Alarm : IEmotion
    {
        public bool Check(EmotionsTypes e)
        {
            return e.Fear > 0.5 && e.Sadness > 0.1 && e.Contempt > 0.09;
        }

        public Emotions GetEmotion()
        {
            return Emotions.Alarm;
        }
    }
}
