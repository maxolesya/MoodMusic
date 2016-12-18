using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data
{
    public class Factory
    {
        static Factory _default;

        public static Factory Default
        {
            get
            {
                if (_default == null)
                    _default = new Factory();
                return _default;
            }
        }

        private IAudioService _audioService;

        public IAudioService GetService(bool vk_auth)
        {
            if (_audioService == null)
            {
                if (vk_auth)
                {
                    _audioService = new VKService();
                }
                else
                {
                    _audioService = new SystemAudio();
                }
            }

            return _audioService;
        }
    }
}
