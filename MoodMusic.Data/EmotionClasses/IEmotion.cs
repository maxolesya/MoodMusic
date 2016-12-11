using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodMusic.Data.EmotionClasses
{
    public enum Emotions
    {
        Happiness,
       
        Anger,
        Sadness,
        Surprise,
        Neutral,
        Fear,
        Disgust,
        Contempt,
        Joy,
        Delight,
        Alarm,
        Depression,
        Despair,
        Confidence,
        Curiosity, Default

    }
    interface IEmotion
    {
       bool Check(EmotionsTypes e);
       Emotions GetEmotion();

    }

    //объект check
    //внутри 
    //цикл по всем классам
    //возвращает эмоции 
    //получию массив даблов каждый из чеков проверяет

    //приним массив возвр фолс

    //второй вернет значение енума эмоции
}
