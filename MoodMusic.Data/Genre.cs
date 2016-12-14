using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodMusic.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}