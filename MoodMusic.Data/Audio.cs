using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data
{
    public class Audio
    {   //https://vk.com/dev/objects/audio_genres

        public int aid { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public string lurics_id { get; set; }
        public int genre { get; set; }
        public override string ToString()
        {
            return artist + " - " + title;
        }
    }

}
