using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodMusic.Data.EmotionClasses;

namespace MoodMusic.Data
{
   public class EmotionGenreRhythmCombination
    {
        Dictionary<Emotions, List<int>> genreEmotionDictionary = new Dictionary<Emotions, List<int>>();
        public Dictionary<Emotions, List<int>> GenreEmotionDictionary
        {
            get
            {               
                return genreEmotionDictionary;
            }
        }
        public EmotionGenreRhythmCombination()
        {
            genreEmotionDictionary.Add(Emotions.Happiness, new List<int> { 1, 4, 5, 1001, 22 });
            genreEmotionDictionary.Add(Emotions.Anger, new List<int> { 1, 7, 21 });
            genreEmotionDictionary.Add(Emotions.Sadness, new List<int> { 2, 6, 11, 14 });
            genreEmotionDictionary.Add(Emotions.Surprise, new List<int> { 5, 17,8,1001,7 });
            genreEmotionDictionary.Add(Emotions.Neutral, new List<int> { 17,4,2 });
            genreEmotionDictionary.Add(Emotions.Fear, new List<int> { 6,11,13,16});
            genreEmotionDictionary.Add(Emotions.Disgust, new List<int> {12 });
            genreEmotionDictionary.Add(Emotions.Contempt, new List<int> { 3,10});
            genreEmotionDictionary.Add(Emotions.Joy, new List<int> { 4, 5, 14 });
            genreEmotionDictionary.Add(Emotions.Alarm, new List<int> { 16,6 });
            genreEmotionDictionary.Add(Emotions.Depression, new List<int> {2,21,13 });
            genreEmotionDictionary.Add(Emotions.Confidence, new List<int> { 3,1,7 });
           
        }

    }
}
