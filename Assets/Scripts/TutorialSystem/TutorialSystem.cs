using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class TutorialSystem : MonoBehaviour
    {
        [SerializeField] private List<TutorialPoint> _tutorialPointsByTimer;
        [SerializeField] private List<TutorialPoint> _tutorialPointsByTrigger;
        [SerializeField] private Image _darkPanel;

        private int _currentPoint = 0;
        private float _startTimeScale;
        private GoldHolder _goldHolder;

        private void Start()
        {
            TutorialPoint.OnTutorialPointComplete += ContinueGame; //ОТПИСАТЬСЯ

            if (SceneContent.Instance.NeedTutorial is false)
            {
                Destroy(gameObject);
                return;
            }

            InitializeTriggers();

            ShowTutorialPoint(_tutorialPointsByTimer[_currentPoint]);
        }

        private void InitializeTriggers()
        {
            if (_tutorialPointsByTrigger.Count == 0)
                return;

            Enemy.OnEnemyDead += OnEnemyDead;
            _goldHolder = FindObjectOfType<GoldHolder>();
            _goldHolder.Gold.OnGoldCountChanged += OnGoldPickUp;
        }

        private void OnGoldPickUp(int obj)
        {
            _goldHolder.Gold.OnGoldCountChanged -= OnGoldPickUp;
            ShowTutorialByType(TutorialType.TakeGold);
        }

        private void OnEnemyDead()
        {
            Enemy.OnEnemyDead -= OnEnemyDead;
            ShowTutorialByType(TutorialType.KillEnemy);
        }

        private void ContinueGame(TutorialPoint point)
        {
            _darkPanel.gameObject.SetActive(false);
            Time.timeScale = _startTimeScale;

            if (point.IsStartByTrigger)
                return;
            StartCoroutine(WaitForNextHelp());
        }

        private void ShowTutorialByType(TutorialType type)
        {
            var tutor = _tutorialPointsByTrigger.Find(x => x.Type == type);

            if (tutor != null)
            {
                _startTimeScale = Time.timeScale;
                Time.timeScale = 0f;
                _darkPanel.gameObject.SetActive(true);
                tutor.StartShow();

                _tutorialPointsByTrigger.Remove(tutor);
            }
        }

        private void ShowTutorialPoint(TutorialPoint point)
        {
            _startTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            _darkPanel.gameObject.SetActive(true);
            point.StartShow();
        }

        private IEnumerator WaitForNextHelp()
        {
            _currentPoint += 1;

            if (_currentPoint >= _tutorialPointsByTimer.Count)
                yield break;

            float delay = _tutorialPointsByTimer[_currentPoint].TimeToShow - _tutorialPointsByTimer[_currentPoint - 1].TimeToShow;
            bool ignoreTimeScale = _tutorialPointsByTimer[_currentPoint].IgnoreTimeScale;

            if (ignoreTimeScale)
            {
                Debug.Log("IGNORE TIME SCALE");
                yield return new WaitForSecondsRealtime(delay);
            }
            else
            {
                Debug.Log("USE TIME SCALE");
                yield return new WaitForSeconds(delay);
            }

            ShowTutorialPoint(_tutorialPointsByTimer[_currentPoint]);
        }

        private void OnDestroy()
        {
            TutorialPoint.OnTutorialPointComplete -= ContinueGame;
        }
    }
}