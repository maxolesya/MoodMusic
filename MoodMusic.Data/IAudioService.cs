using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data
{
    public interface IAudioService
    {
        List<Audio> AudioList { get;  }
        void GetAudioList(string id, string token);
    }
}
