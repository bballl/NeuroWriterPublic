using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    public class EnemySpawner : MonoBehaviour
    {
        public bool _isManual;

        [Header("Character")]
        [SerializeField] private MainCharacterController _character;
        [Header("Enemy")]
        [SerializeField] private EnemyFactory _enemyFactory;
        [Header("Timer")]
        [SerializeField] private TimerController _timerController;
        [Header("Borders")]
        [Header("Data Servers")]
        [SerializeField] private PropWithItem _dataServerPrefab;


        [Header("Other")]
        public int count = 10;
        public float radiusForItems = 15;
        public float radiusForEnemies = 15;

        private BorderChecker _locationBorderChecker;
        private DataServerData _dataServerData;
        private bool _isReady = false;
        private Timer _timer;
        private int _currentTimeLevel = 0;
        private WaitForSeconds _dataServerSpawnDelay;

        private void Start()
        {
            _locationBorderChecker = SceneContent.Instance.GetSceneLocation();

            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            while(_isReady is false)
            {
                if (_enemyFactory == null)
                {
                    Debug.Log($"{name} waiting EnemyFactory");
                    yield return new WaitForFixedUpdate();
                }
                else if (_enemyFactory.Initialized is false)
                {
                    Debug.Log($"{name} waiting EnemyFactory is initialized");
                    yield return new WaitForFixedUpdate();
                }
                else if (_timerController == null)
                {
                    Debug.Log($"{name} waiting TimerController");
                    yield return new WaitForFixedUpdate();
                }
                else if (_timerController.Timer == null)
                {
                    Debug.Log($"{name} waiting Timer in TimerController");
                    yield return new WaitForFixedUpdate();
                }
                else if (_locationBorderChecker == null)
                {
                    Debug.Log($"{name} waiting BorderChecker");
                    yield return new WaitForFixedUpdate();
                }

                _isReady = true;
            }

            Construct();
        }

        private void Construct()
        {
            _dataServerData = SceneContent.Instance._dataServerData;
            _dataServerSpawnDelay = new WaitForSeconds(_dataServerData.TimeBetweenSpawn);

            Debug.Log("ConstructInEnemySpawner");
            _timer = _timerController.Timer;

            _timer.OnNewMinuteStarted += OnNewMinuteStarted;
            Timer.OnTimeEnded += Timer_OnTimeEnded;

            if (!_isManual)
                StartSpawn(); 
        }

        private void Timer_OnTimeEnded()
        {
            StopAllCoroutines();
        }

        private void OnNewMinuteStarted(int minute)
        {
            _currentTimeLevel++;
        }

        private void OnDestroy()
        {
            _timer.OnNewMinuteStarted -= OnNewMinuteStarted;
            Timer.OnTimeEnded -= Timer_OnTimeEnded;
        }

        private void StartSpawn()
        {
            StartCoroutine(SpawnNumber());
            StartCoroutine(SpawnLetter());
            StartCoroutine(SpawnWord());
            StartCoroutine(SpawnDataServer());
        }

        private IEnumerator SpawnNumber()
        {
            while (true)
            {
                float delay = _enemyFactory.GetEnemyDelay(_currentTimeLevel, EnemyType.Number);
                yield return new WaitForSeconds(delay);
                Vector2 point = _locationBorderChecker.GetRandomOverCircle(_character.transform.position, radiusForEnemies);
                Enemy enemy = _enemyFactory.CreateNumberEnemy(_currentTimeLevel);
                enemy.SetTargetToFollow(_character.transform);
                enemy.transform.position = point;
                enemy.transform.SetParent(transform);
            }
        }

        private IEnumerator SpawnLetter()
        {
            while (true)
            {
                float delay = _enemyFactory.GetEnemyDelay(_currentTimeLevel, EnemyType.Letter);
                yield return new WaitForSeconds(delay);
                Vector2 point = _locationBorderChecker.GetRandomOverCircle(_character.transform.position, radiusForEnemies);
                Enemy enemy = _enemyFactory.CreateRandomLetter(_currentTimeLevel);
                enemy.SetTargetToFollow(_character.transform);
                enemy.transform.position = point;
                enemy.transform.SetParent(transform);
            }
        }

        private IEnumerator SpawnWord()
        {
            while (true)
            {
                float delay = _enemyFactory.GetEnemyDelay(_currentTimeLevel, EnemyType.Word);
                yield return new WaitForSeconds(delay);
                Vector2 point = _locationBorderChecker.GetRandomOverCircle(_character.transform.position, radiusForEnemies);
                Enemy enemy = _enemyFactory.CreateWordEnemy(_currentTimeLevel);
                enemy.SetTargetToFollow(_character.transform);
                enemy.transform.position = point;
                enemy.transform.SetParent(transform);
            }
        }

        private IEnumerator SpawnDataServer()
        {
            while (true)
            {
                yield return _dataServerSpawnDelay;
                Vector2 point = _locationBorderChecker.GetRandomPositionNear(_character.transform.position, radiusForItems);
                var item = Instantiate(_dataServerPrefab, point, Quaternion.identity, transform);
                item.Init(_dataServerData);
            }
        }

        private void Update()
        {
            if (_isManual)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Vector2 point = _locationBorderChecker.GetRandomPosition();
                    Enemy enemy = _enemyFactory.CreateNumberEnemy(_currentTimeLevel);
                    enemy.SetTargetToFollow(_character.transform);
                    enemy.transform.position = point;
                    enemy.transform.SetParent(transform);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Vector2 point = _locationBorderChecker.GetRandomPosition();
                    Enemy enemy = _enemyFactory.CreateRandomLetter(_currentTimeLevel);
                    enemy.SetTargetToFollow(_character.transform);
                    enemy.transform.position = point;
                    enemy.transform.SetParent(transform);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    Vector2 point = _locationBorderChecker.GetRandomPosition();
                    Enemy enemy = _enemyFactory.CreateWordEnemy(_currentTimeLevel);
                    ((EnemyWord)enemy).SetTargetToFollow(_character.transform);
                    enemy.transform.position = point;
                    enemy.transform.SetParent(transform);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    Vector2 point = _locationBorderChecker.GetRandomPosition();
                    var item = Instantiate(_dataServerPrefab, point, Quaternion.identity, transform);
                    item.Init(_dataServerData);
                }
            }
        }
    }
}