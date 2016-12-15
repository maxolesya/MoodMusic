using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    class Depression : IEmotion
    {
        public bool Check(EmotionsTypes e)
        {
           return e.Sadness>0.6 && e.Fear>0.01;
        }

        public Emotions GetEmotion()
        {
            throw new NotImplementedException();
        }
    }
}
