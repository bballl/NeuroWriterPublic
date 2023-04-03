using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameBoxProject
{
    public class WordGiver : MonoBehaviour
    {
        private AllWords _obligatoryWords;
        private AllWords _otherWords;

        private static Dictionary<string, WordType> _wordsToGive = new();
        private static AllWords _otherWordsStatic;

        private void Awake()
        {
            _obligatoryWords = SceneContent.Instance._obligatoryWords;
            _otherWords = SceneContent.Instance._otherWords;

            Initialize(_obligatoryWords);
            _otherWordsStatic = _otherWords;

            EnemyWord.OnWordDead += DeleteWord;
            WordPanel.OnBuffWordComplete += DeleteWord;
        }

        private void DeleteWord(string word, WordType type)
        {
            if (_wordsToGive.ContainsKey(word))
                _wordsToGive.Remove(word);
        }

        private static void Initialize(AllWords listWithWords)
        {
            foreach (var wordList in listWithWords.Words)
            {
                foreach (var word in wordList.Words)
                {
                    if (_wordsToGive.ContainsKey(word))
                        continue;

                    _wordsToGive.Add(word, wordList.Type);
                }
            }
            Debug.Log($"{_wordsToGive.Count} words are added to base");
        }

        public static bool TryGetWord(out WordType type, out string word)
        {
            if (_wordsToGive.Count == 0)
            {
                Initialize(_otherWordsStatic);
            }

            List<string> keys = _wordsToGive.Keys.ToList();
            int randIndex = Random.Range(0, _wordsToGive.Count);

            word = keys[randIndex];
            type = _wordsToGive[word];

            return true;
        }

        public static bool TryGetWordNecessary(List<string> usedWords, out WordType type, out string word)
        {
            foreach (var givenWord in _wordsToGive.Keys)
            {
                if (usedWords.Contains(givenWord) is false)
                {
                    type = _wordsToGive[givenWord];
                    word = givenWord;
                    return true;
                }
            }

            //var randType = Random.Range(0, _otherWordsStatic.Words.Count);
            //type = _otherWordsStatic.Words[randType].Type;

            //var wordIndex = Random.Range(0, _otherWordsStatic.Words[randType].Words.Count);
            //word = _otherWordsStatic.Words[randType].Words[wordIndex];

            //return true;

            Initialize(_otherWordsStatic);

            foreach (var givenWord in _wordsToGive.Keys)
            {
                if (usedWords.Contains(givenWord) is false)
                {
                    type = _wordsToGive[givenWord];
                    word = givenWord;
                    return true;
                }
            }

            type = default;
            word = null;

            throw new System.Exception("ERROR WHEN TRY TO GET NEW WORD BY METOD [TRY GET WORD NECESSARY]");
        }
    }
}