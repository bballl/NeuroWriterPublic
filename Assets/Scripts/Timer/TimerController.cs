using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class TimerController : MonoBehaviour
    {
        private int _minutes;
        private int _seconds;

        public Timer Timer { get; private set; }

        private void Start()
        {
            _minutes = SceneContent.Instance._minutes;
            _seconds = SceneContent.Instance._seconds;

            Timer = new Timer(_minutes, _seconds);

            Timer.OnNewMinuteStarted += Timer_OnNewMinuteStarted;
        }

        private void Timer_OnNewMinuteStarted(int minute)
        {
            Debug.Log($"Началась {minute}-я минута");
        }

        private void FixedUpdate()
        {
            if (Timer != null)
                Timer.DecreaseTime(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            Timer.OnNewMinuteStarted -= Timer_OnNewMinuteStarted;
        }
    }
}