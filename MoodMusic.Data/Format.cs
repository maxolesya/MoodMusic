using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data
{
    public class Format
    {
        public int Id { get; set; }
        public string FormatName { get; set; }
    }
}
