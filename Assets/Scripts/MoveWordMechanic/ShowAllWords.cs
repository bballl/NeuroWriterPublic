using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameBoxProject
{
    public class ShowAllWords : MonoBehaviour
    {
        public WordHolder WordHolder;
        public TextPlank Prefab;
        public FramesColorer FramesColorer;

        private WordsColors _wordsColors;
        private Dictionary<string, WordType> _allWords = new();


        private void Start()
        {
            _wordsColors = SceneContent.Instance._wordsColors;
            _allWords = WordsContainer.GetWords();

            List<string> needWords = new();

            foreach (var word in _allWords.Keys)
            {
                if (_allWords[word] == WordHolder.Type)
                {
                    needWords.Add(word);
                }
            }

            foreach (var word in needWords)
            {
                var obj = Instantiate(Prefab, transform);
                var color = _wordsColors.WordColors.Find(x => x.WordType == WordHolder.Type).Color;
                obj.Init(color, word, WordHolder.Type);
                FramesColorer.ColorFrames(color);
            }
        }
    }
}