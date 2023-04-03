using System.Collections;
using UnityEngine;
using TMPro;

namespace GameBoxProject
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TimerController _timerController;

        private Timer _timer;
        private bool _isReady = false;

        private void Start() => 
            StartCoroutine(WaitCoroutine());

        private IEnumerator WaitCoroutine()
        {
            while (_isReady is false)
            {
                if (_timerController == null)
                {
                    Debug.Log($"{name} waiting for TimerController");
                    yield return new WaitForFixedUpdate();
                }
                else if (_timerController.Timer == null)
                {
                    Debug.Log($"{name} waiting for Timer in TimerController");
                    yield return new WaitForFixedUpdate();
                }

                _isReady = true;
            }

            Construct();
        }

        private void Construct()
        {
            _timer = _timerController.Timer;

            _timer.OnTimeChanged += OnTimerValueChanged;

            OnTimerValueChanged(_timer.GetTime());
        }

        private void OnTimerValueChanged(string time)
        {
            _timerText.text = time;
        }

        private void OnDestroy()
        {
            _timer.OnTimeChanged -= OnTimerValueChanged;
        }
    }
}