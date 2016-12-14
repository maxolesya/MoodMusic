using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data
{
   public class EmotionsTypes
    {
        public float Happiness { get; set; }
        public float Fear { get; set; }
        public float Anger { get; set; }
        public float Contempt { get; set; }
        public float Disgust { get; set; }
        public float Sadness { get; set; }
        public float Neutral { get; set; }
        public float Surprise { get; set; }
        public override string ToString()
        {
            
            return "happiness" + Happiness + " fear" + Fear + " " + Anger+" surprise "+Surprise;
        }
    }
    
}
