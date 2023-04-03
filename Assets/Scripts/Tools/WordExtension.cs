using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    static class WordExtension
    {
        public static List<Letter> ToLetters(this string word, LetterData allLetters)
        {
            word = word.ToLower();
            List<Letter> result = new List<Letter>();
            var availableLetters = allLetters.Letters;

            for (int i = 0; i < word.Length; i++)
            {
                var letter = availableLetters.Find(x => x.Name.Equals(word[i]));
                if (letter == null)
                {
                    Debug.LogError($"Word [{word}] have unavailable symbol - [{word[i]}]");
                    return null;
                }
                result.Add(letter);

            }

            return result;
        }
    }
}
