using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Ability;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameBoxProject
{
    public class WordPanel : MonoBehaviour
    {
        public static event Action<string, WordType> OnBuffWordComplete;

        public Transform Parent;

        [SerializeField] private LetterIcon LetterPrefab;
        [SerializeField] private AbilityType _abilityType;

        private Dictionary<LetterIcon, char> _letterImages = new();
        private string _word;
        private WordType _type;

        public void CheckDeadLetter(Letter obj)
        {
            if (_letterImages.Count > 0)
            {
                foreach (var icon in _letterImages.Keys)
                {
                    if (icon.IsHiden)
                    {
                        if (_letterImages[icon] == obj.Name)
                        {
                            icon.Show(obj.Name);
                        }
                    }
                }
                CheckWordIsReady();
            }
        }

        private void CheckWordIsReady()
        {
            foreach (var icon in _letterImages.Keys)
            {
                if (icon.IsHiden)
                    return;
            }

            OnBuffWordComplete?.Invoke(_word, _type);
            Observer.ActivationAbilityEvent.Invoke(_abilityType);

            foreach (var go in _letterImages.Keys)
            {
                Destroy(go.gameObject);
            }
            _letterImages.Clear();
        }

        public void SetWord(string word, WordType type)
        {
            _word = word;
            _type = type;

            for (int i = 0; i < word.Length; i++)
            {
                var image = Instantiate(LetterPrefab, Parent);
                _letterImages[image] = word[i];
            }
        }

        public bool HasWord(string word)
        {
            if (_word == null)
                return false;

            return _word.Equals(word);
        }

        public bool IsActive(out string word)
        {
            if (_word != string.Empty)
            {
                word = _word;
                return true;
            }
            else
            {
                word = string.Empty;
                return false;
            }
        }
    }
}