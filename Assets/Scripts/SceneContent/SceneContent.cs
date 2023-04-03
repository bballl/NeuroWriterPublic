using Assets.Scripts.MenuAndUI;
using Assets.Scripts.Units.Character;
using UnityEngine;

namespace GameBoxProject
{
    public class SceneContent : MonoBehaviour
    {
        public static SceneContent Instance { get; private set; }

        public int LevelIndex;

        [Header("Character")]
        public CharacterInitData _characterData;

        [Header("Words")]
        public AllWords _obligatoryWords;
        public AllWords _otherWords;

        [Header("Narrative text")]
        public Narrative _narrative;

        [Header("Dara-server")]
        public DataServerData _dataServerData;

        [Header("Experience and Level")]
        public LevelData _levelData;

        [Header("Letters and Numbers")]
        public LetterData _letters;
        public LetterData _numbers;

        [Header("Enemy")]
        public EnemyData _enemyData;

        [Header("EnemyNumbers")]
        public EnemyDataByMinutes _numbersDataByMinute;

        [Header("EnemyLetters")]
        public EnemyDataByMinutes _lettersDataByMinute;

        [Header("EnemyWords")]
        public EnemyDataByMinutes _wordsDataByMinute;
        public WordAttackData _wordAttack;

        [Header("Words Colors")]
        public WordsColors _wordsColors;

        [Header("Timer")]
        public int _minutes;
        [Range(0, 59)]
        public int _seconds;

        [Header("LocationPrefab")]
        public BorderChecker _locationPrefab;

        [Header("Reward")]
        public RoundReward _rewardData;

        public bool NeedTutorial = false;


        public BorderChecker CreatedLocation { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                MemoryProgressPanel.OnRoundComplete += OnRoundComplete;
                SceneController.OnSceneStartLoad += SceneController_OnSceneStartLoad;
            }
            else
                Destroy(this.gameObject);
        }

        public BorderChecker GetSceneLocation()
        {
            if (CreatedLocation == null)
                CreatedLocation = Instantiate(_locationPrefab, Vector2.zero, Quaternion.identity);

            return CreatedLocation;
        }

        private void SceneController_OnSceneStartLoad(Scenes obj)
        {
            if (obj.Equals(Scenes.MainMenu))
                OnRoundComplete();
        }

        private void OnRoundComplete()
        {
            Debug.Log("Round is completed or you back to main Menu. Delete SceneContent");
            
            MemoryProgressPanel.OnRoundComplete -= OnRoundComplete;
            SceneController.OnSceneStartLoad -= SceneController_OnSceneStartLoad;

            Destroy(this.gameObject);
        }
    }
}