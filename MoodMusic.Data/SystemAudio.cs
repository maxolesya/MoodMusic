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

        private  List<Genre> genres;

        public List<Genre> Genres
        {
            get
            {
                return genres;
            }
        }
        public SystemAudio()
        {
            genres = new List<Genre>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourse = "MoodMusic.Data.Data.csv";
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourse)))
                {
                    CsvReader csv = new CsvReader(reader);
                    csv.Configuration.WillThrowOnMissingField = false;
                    csv.Configuration.Delimiter = ";";
                    genres.AddRange(csv.GetRecords<Genre>().ToArray());
                }
               
            
        }
        public List<string> mediaExtensions = new List<string> { ".mp3", ".mp4" };
        public List<string> filesfound = new List<string>();

        public void GetAudioList(string id, string token)
        {
            audiolist = new List<Audio>();
            //string path = dwindow.GetPath();
            DirSearch(token);
            foreach (var item in filesfound)
            {
                TagLib.File tagFile = TagLib.File.Create(item);
                int i = 0;
                int _genre = 0;
                try
                {
                    _genre = genres.FirstOrDefault(g => g.Name.Equals(tagFile.Tag.FirstGenre == null ? "" : tagFile.Tag.FirstGenre)).Id;
                }
                catch (ArgumentNullException)
                {

                }
                Audio audio = new Audio
                {
                    aid = i,
                    artist = tagFile.Tag.FirstPerformer == null ? "" : tagFile.Tag.FirstPerformer,
                    title = tagFile.Tag.Title == null ? "" : tagFile.Tag.Title,
                    duration = (int)(tagFile.Properties.Duration.TotalSeconds),
                    url = item,
                    genre = _genre
                };
                audiolist.Add(audio);
                i++;
            }
        }

        public void DirSearch(string sDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sDir);

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
