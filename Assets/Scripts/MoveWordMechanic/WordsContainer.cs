using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class WordsContainer : MonoBehaviour
    {
        protected static Dictionary<string, WordType> _container = new();

        private void Awake()
        {
            _container.Clear();
            EnemyWord.OnWordDead += AddWord;
            WordPanel.OnBuffWordComplete += AddWord;
        }

        public static void AddWord(string word, WordType type)
        {
            if (_container.ContainsKey(word))
            {
                Debug.Log($"Container is already has [{word}]");
                return;
            }

            _container.Add(word, type);
            Debug.Log($"Word [{word}] added to Container");
        }

        public static Dictionary<string, WordType> GetWords()
        {
            return _container;
        }

        private void OnDestroy()
        {
            EnemyWord.OnWordDead -= AddWord;
            WordPanel.OnBuffWordComplete -= AddWord;
        }
    }
}