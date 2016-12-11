﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    public class Contempt : IEmotion
    {
        public bool Check(EmotionsTypes e)
        {
            return e.Contempt > 0.9;
        }

        public Emotions GetEmotion()
        {
            return Emotions.Contempt;
        }
    }
}
