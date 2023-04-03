using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameBoxProject
{
    public class TestAddWordToPanel : MonoBehaviour
    {
        [SerializeField] private TextPlank _textPlank;
        [SerializeField] private TMP_Text _textPlace;

        [SerializeField] private SelectedWordsHolder _selectedWordsHolder;

        public Transform ParentPanel;

        private AllWords _obligatoryWords;
        private AllWords _otherWords;
        private WordsColors _wordsColors;
        private Narrative _narrative;


        private List<string> _secretWords = new(); 
        private List<string> _selectedWords = new(); // ‚ÁˇÚ¸ ËÁ SelectedWordsHolder

        private Dictionary<string, bool> _words;

        private void Start()
        {
            _obligatoryWords = SceneContent.Instance._obligatoryWords;
            _otherWords = SceneContent.Instance._otherWords;
            _wordsColors = SceneContent.Instance._wordsColors;
            _narrative = SceneContent.Instance._narrative;


            Debug.Log($"There is {WordsContainer.GetWords().Count} words in container");

            _selectedWordsHolder.OnWordsChanged += RefreshText;
            AddText();
        }

        private void RefreshText(List<string> words)
        {
            _selectedWords.Clear();
            string result = string.Empty;
            int counter = 0;

            foreach (var txt in _words.Keys)
            {
                if (_words[txt])
                {
                    if (words[counter] != string.Empty)
                    {
                        _selectedWords.Add(words[counter]);
                        result = $"{result} <color=#BDB76B>{words[counter]}</color>";
                    }
                    else
                    {
                        _selectedWords.Add(string.Empty);
                        result = $"{result} <color=#800000> ¬€¡≈–»“≈ —ÀŒ¬Œ </color>";
                    }
                    counter++;
                }
                else
                {
                    result = result + txt;
                }
            }

            _textPlace.text = result;
        }

        [ContextMenu("Test add text")]
        public void AddText()
        {
            _words = _narrative.Text.CryptText();             // “ÛÚ Ì‡‰Ó ÔÓ‰ÛÏ‡Ú¸ ‚ ·Û‰Û˘ÂÏ

            AllWords allWords = _obligatoryWords + _otherWords;

            string result = string.Empty;
            foreach (var txt in _words.Keys)
            {
                if (_words[txt])
                {
                    if (allWords.HasWord(txt, out WordType type))
                    {
                        result = $"{result} <color=#800000> ¬€¡≈–»“≈ —ÀŒ¬Œ </color>";
                        _secretWords.Add(txt);
                        Debug.LogWarning($"Word [{txt}] added to SecretList");
                    }
                    else
                        Debug.LogError($"There is no word [{txt}] in Words DataBase");
                }
                else
                {
                    result = result + txt;
                }
            }

            _selectedWordsHolder.Init(_secretWords);

            _textPlace.text = result;
        }

        public float GetResult()
        {
            int right = 0;
            for (int i = 0; i < _secretWords.Count; i++)
            {
                if (i >= _selectedWords.Count)
                    break;

                if (_secretWords[i].Equals(_selectedWords[i]))
                {
                    Debug.Log($"{_secretWords[i]} is equals {_selectedWords[i]}"); 
                    right++;
                }
            }

            Debug.Log($"SecretWordsCount = {_secretWords.Count}");
            Debug.Log($"SelectedWordsCount = {_selectedWords.Count}");
            Debug.Log($"Right = {right}/{_secretWords.Count}");
            float value = (float)right / (float)_secretWords.Count;
            return value;
        }
    }
}