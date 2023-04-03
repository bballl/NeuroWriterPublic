using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private LetterData _letters;
        [SerializeField] private LetterData _numbers;

        public bool Initialized { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private EnemyNumber _enemyNumberPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private EnemyWordPart _enemyWordHeadPrefab;
        [SerializeField] private EnemyWordPart _enemyWordPartPrefab;
        [SerializeField] private EnemyWord _enemyWordPrefab;


        private EnemyDataByMinutes _numbersEnemyDatasByMinutes;
        private EnemyDataByMinutes _lettersEnemyDatasByMinutes;
        private EnemyDataByMinutes _wordsEnemyDatasByMinutes;
        private EnemyData _enemyData;


        private Pool<Enemy> _enemyPool;
        private Pool<Enemy> _enemyNumberPool;
        private Pool<EnemyWordPart> _enemyWordHeadPool;
        private Pool<EnemyWordPart> _enemyWordPartPool;
        private Pool<EnemyWord> _enemyWordPool;

        private void Start()
        {
            _numbersEnemyDatasByMinutes = SceneContent.Instance._numbersDataByMinute;
            _lettersEnemyDatasByMinutes = SceneContent.Instance._lettersDataByMinute;
            _wordsEnemyDatasByMinutes = SceneContent.Instance._wordsDataByMinute;
            _enemyData = SceneContent.Instance._enemyData;


            _enemyPool = new Pool<Enemy>(200, _enemyPrefab, true);
            _enemyNumberPool = new Pool<Enemy>(100, _enemyNumberPrefab, true);
            _enemyWordHeadPool = new Pool<EnemyWordPart>(5, _enemyWordHeadPrefab, true);
            _enemyWordPartPool = new Pool<EnemyWordPart>(50, _enemyWordPartPrefab, true);
            _enemyWordPool = new Pool<EnemyWord>(5, _enemyWordPrefab, true);

            Initialized = true;
        }

        public Enemy CreateNumberEnemy(int minuteInGame)
        {
            var randNumber = _numbers.Letters[Random.Range(0, _numbers.Letters.Count)];
            Enemy enemy = _enemyNumberPool.GetObject();

            enemy.InitModel(randNumber);
            enemy.Init(_enemyData, _numbersEnemyDatasByMinutes.DataByMinutes[minuteInGame]);
            return enemy;
        }

        public Enemy CreateLetterEnemy(char letter, int minuteInGame)
        {
            var data = _letters.Letters.Find(x => x.Name.Equals(letter));

            Enemy enemy = _enemyPool.GetObject();

            enemy.InitModel(data);
            enemy.Init(_enemyData, _lettersEnemyDatasByMinutes.DataByMinutes[minuteInGame]);
            return enemy;
        }

        public Enemy CreateRandomLetter(int minuteInGame)
        {
            var data = _letters.Letters[Random.Range(0, _letters.Letters.Count)];

            Enemy enemy = _enemyPool.GetObject();

            enemy.InitModel(data);
            enemy.Init(_enemyData, _lettersEnemyDatasByMinutes.DataByMinutes[minuteInGame]);
            return enemy;
        }

        public EnemyWord CreateWordEnemy(int minuteInGame, List<Letter> word = null)
        {
            string text = string.Empty;
            WordType type = WordType.any;

            if (word == null)
            {
                //var wordList = _words.Words[Random.Range(0, _words.Words.Count)];

                //text = wordList.Words[Random.Range(0, wordList.Words.Count)];
                if (WordGiver.TryGetWord(out type, out text))
                    word = text.ToLetters(_letters);
                else
                    Debug.LogError($"Error to try get word");
            }

            var head = _enemyWordHeadPool.GetObject();

            EnemyWord enemy = _enemyWordPool.GetObject();
            enemy.Init(_enemyData, _wordsEnemyDatasByMinutes.DataByMinutes[minuteInGame]);
            enemy.SetWord(text, type);
            
            head.transform.SetParent(enemy.transform);
            head.InitModel(word[0]);
            head.Init(_enemyData, _lettersEnemyDatasByMinutes.DataByMinutes[minuteInGame], enemy.Health);

            enemy.AddLetter(head);

            for (int i = 1; i < word.Count; i++)
            {
                var letter = _enemyWordPartPool.GetObject();
                letter.InitModel(word[i]);
                letter.Init(_enemyData, _lettersEnemyDatasByMinutes.DataByMinutes[minuteInGame], enemy.Health);

                letter.transform.SetParent(enemy.transform);

                enemy.AddLetter(letter);
            }

            enemy.InitAttackAndHealth();

            Debug.Log($"Created enemy - [{text}]");
            return enemy;
        }

        public float GetEnemyDelay(int minuteInGame, EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Number:
                    return 1 / _numbersEnemyDatasByMinutes.DataByMinutes[minuteInGame].SpawnCountInSecond;

                case EnemyType.Letter:
                    return 1 / _lettersEnemyDatasByMinutes.DataByMinutes[minuteInGame].SpawnCountInSecond;

                case EnemyType.Word:
                    return 1 / _wordsEnemyDatasByMinutes.DataByMinutes[minuteInGame].SpawnCountInSecond;

                default:
                    return 1f;
            }
        }
    }
}