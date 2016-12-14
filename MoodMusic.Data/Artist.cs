using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodMusic.Data
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}