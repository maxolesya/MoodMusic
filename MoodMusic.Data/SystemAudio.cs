using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TagLib;


namespace MoodMusic.Data
{
    public class SystemAudio : IAudioService
    {

        private List<Audio> audiolist;

        public List<Audio> AudioList
        {
            get
            {
                return audiolist;
            }
        }

        private static List<Genre> genres;

        public List<Genre> Genres
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourse = "MoodMusic.Data.Data.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourse))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        CsvReader csv = new CsvReader(reader);
                        csv.Configuration.WillThrowOnMissingField = false;
                        csv.Configuration.Delimiter = ";";
                        genres.AddRange(csv.GetRecords<Genre>().ToArray());
                    }
                    return genres;
                }
            }
        }

        public List<string> mediaExtensions = new List<string> { ".mp3", ".mp4" };
        public List<string> filesfound = new List<string>();

        public void GetAudioList(string id, string token)
        {
            //string path = dwindow.GetPath();
            DirSearch(token);
            foreach (var item in filesfound)
            {
                TagLib.File tagFile = TagLib.File.Create(item);
                int i = 0;
                Audio audio = new Audio
                {
                    aid = i,
                    artist = tagFile.Tag.FirstPerformer,
                    title = tagFile.Tag.Title,
                    duration = (int)(tagFile.Properties.Duration.TotalSeconds),
                    url = item,
                    genre = genres.FirstOrDefault(g => g.Name.Equals(tagFile.Tag.FirstGenre)).Id
                };
                audiolist.Add(audio);
                i++;
            } 
        }

        public void DirSearch(string sDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sDir);
            int i = 0;
            audiolist = new List<Audio>();
            foreach (var item in dir.GetDirectories())
            {
                if (!item.FullName.Equals(@"C:\Windows"))
                {
                    try
                    {
                        foreach (var f in item.GetFiles())
                        {
                            if (mediaExtensions.Contains(f.Extension))
                            {
                                filesfound.Add(f.FullName);
                            }
                        }
                        DirSearch(item.FullName);
                    }
                    catch (System.UnauthorizedAccessException)
                    {

                    }
                }
            }
        }

    }
}
