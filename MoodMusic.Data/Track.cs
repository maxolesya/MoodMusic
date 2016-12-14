using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data
{
    public class Track
    {
        public int Id { get; set; }
        public virtual Genre Genre { get; set; }
        public int Duration { get; set; }
        [Key]
        [Column(Order =1)]
        public string Title { get; set; }
        [Column(Order =2)]
        public virtual Artist Artist { get; set; }
        public Format Format { get; set; }
        public string Path { get; set; }
    }
}
