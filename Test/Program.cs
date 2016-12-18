using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodMusic.Data;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemAudio s = new SystemAudio();
            string path = @"C:\Олеся\музыка vk";
            s.GetAudioList("",path);
            Console.ReadLine();
        }
    }
}
