namespace MoodMusic.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoodMusic.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public static List<string> mediaExtensions = new List<string> { ".mp3", ".mp4" };
        public static List<string> filesFound = new List<string>();

        public static List<string> DirSearch(string sDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sDir);
            foreach (var item in dir.GetDirectories())
            {
                try
                {
                    foreach (var f in item.GetFiles())
                    {
                        if (mediaExtensions.Contains(f.Extension))
                            filesFound.Add(f.FullName);
                    }
                    DirSearch(item.FullName);
                }
                catch (System.UnauthorizedAccessException)
                {

                }
            }
            return filesFound;
        }
        protected override void Seed(Context context)
        {
            DirSearch(@"C:\");
            int i = 1;
            foreach (string audio in filesFound)
            {
                TagLib.File tagFile = TagLib.File.Create(audio);
                Format form = new Format
                {
                    Id = i,
                    FormatName = Path.GetExtension(audio)
                };
                context.Formats.AddOrUpdate(f => f.FormatName, form);
                i++;
                Artist art = new Artist
                {
                    Id = i,
                    Name = tagFile.Tag.FirstPerformer
                };
                context.Artists.AddOrUpdate(a => a.Name, art);
                i++;
                Genre gen = new Genre
                {
                    Id = i,
                    Name = tagFile.Tag.FirstGenre
                };
                context.Genres.AddOrUpdate(g => g.Name, gen);
                i++;
                Track tr = new Track
                {
                    Id = i,
                    Genre = context.Genres.FirstOrDefault(g => g.Name.Equals(gen.Name)),
                    Duration = (int)(tagFile.Properties.Duration.TotalSeconds),
                    Artist = context.Artists.FirstOrDefault(a => a.Name.Equals(art.Name)),
                    Format = context.Formats.FirstOrDefault(f => f.FormatName.Equals(form.FormatName)),
                    Path = audio,
                    Title = tagFile.Tag.Title
                };
                context.Tracks.AddOrUpdate(t => new { t.Title, t.Artist }, tr);
                i++;
            }
        }
    }
}
