using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class SelectedWordsHolder : MonoBehaviour
    {
        public event Action<List<string>> OnWordsChanged;

        [SerializeField] private TextPlank secretWordPrefab;
        
        private WordsColors _wordColor;
        private TextPlank[] _planks;

        public void Init(List<string> words)
        {
            _wordColor = SceneContent.Instance._wordsColors;

            _planks = new TextPlank[words.Count];
            WordMover.OnWordEndDrag += Refresh;

            for (int i = 0; i < words.Count; i++)
            {
                _planks[i] = Instantiate(secretWordPrefab, transform);
                var color = _wordColor.WordColors.Find(x => x.WordType == WordType.any).Color;
                _planks[i].Init(color, "---якнбн---", WordType.any);
                var secret = _planks[i].gameObject.AddComponent<SecretWordHolder>();
                secret.Init(words[i], WordType.any);
                _planks[i].gameObject.GetComponent<WordMover>().enabled = false;
            }
        }

        private void Refresh()
        {
            OnWordsChanged?.Invoke(GetWords());
        }


        public List<string> GetWords()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy is false)
                {
                    continue;
                }

                else if (transform.GetChild(i).TryGetComponent(out TextPlank plank))
                {
                    Debug.Log($"Get word [{plank}] with type [{plank.WordType}]");
                    if (plank.WordType == WordType.any)
                        result.Add(string.Empty);
                    else
                        result.Add(plank.ToString());
                }
            }
            Debug.Log($"Return List with {result.Count} elements");

            return result;
        }

        private void OnDestroy()
        {
            WordMover.OnWordEndDrag -= Refresh;
        }
    }
}