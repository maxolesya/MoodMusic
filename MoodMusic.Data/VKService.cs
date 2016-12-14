using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace MoodMusic.Data
{
    public class VKService
    {
        public event Action<List<Audio>> onAudioListDownloaded;
        private List<Audio> audiolist;

        public List<Audio> AudioList
        {
            get { return audiolist; }
           
        }

        public void GetAudioList( string id, string token)
        {           
          
            using (var client = new HttpClient())
            {

                var result =  client.GetStringAsync("https://api.vk.com/method/audio.get?owner_id=" +id + "&need_user=0&access_token=" +token).Result;
                JToken jtoken = JToken.Parse(result);
                audiolist = jtoken["response"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();
                if (audiolist.Count==0)
                {
                    throw new ArgumentException("В Вашем аккаунте VK нет аудиозаписей");
                }
                if (onAudioListDownloaded != null)
                {
                    onAudioListDownloaded(audiolist);
                }

            }
        }
    }
}
