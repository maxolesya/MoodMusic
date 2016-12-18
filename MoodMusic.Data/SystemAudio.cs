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

        private List<Audio> audiolist = new List<Audio>();

        public List<Audio> AudioList
        {
            get
            {
                return audiolist;
            }
        }

        private List<Genre> genres;

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
            string resourse = "MoodMusic.Data.Data.Formats.csv";
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
            int i = 0;
            DirectoryInfo defdir = new DirectoryInfo(@"C:\Users\belousovnikita\Source\Repos\MoodMusic\MoodMusic.Data\DefaultMusic");
            foreach (var folder in defdir.GetDirectories())
            {
                foreach (var file in folder.GetFiles())
                {
                    TagLib.File tagf = TagLib.File.Create(file.FullName);
                    Audio audio = new Audio
                    {
                        aid = i,
                        artist = tagf.Tag.FirstComposer,
                        title = tagf.Tag.Title,
                        duration = (int)(tagf.Properties.Duration.TotalSeconds),
                        url = file.FullName,
                        genre = genres.FirstOrDefault(g => g.Name.Equals(tagf.Tag.FirstGenre == null ? "" : tagf.Tag.FirstGenre)).Id
                    };
                    audiolist.Add(audio);
                    i++;
                }
            }
            DirSearch(token);
            foreach (var item in filesfound)
            {
                TagLib.File tagFile = TagLib.File.Create(item);

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
            if (dir.GetDirectories().Count() != 0)
            {
                if (!dir.FullName.Equals(@"C:\Windows"))
                {
                    foreach (var item in dir.GetDirectories())
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
            else
            {
                try
                {
                    foreach (var f in dir.GetFiles())
                    {
                        if (mediaExtensions.Contains(f.Extension))
                        {
                            filesfound.Add(f.FullName);
                        }
                    }
                }
                catch (System.UnauthorizedAccessException)
                {

                }
            }
        }

    }
}
