using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class TutorialPoint : MonoBehaviour
    {
        public static event Action<TutorialPoint> OnTutorialPointComplete;

        [Tooltip("Time in seconds from start game to show Help")]
        [field: SerializeField] public float TimeToShow { get; private set; }
        [field: SerializeField] public TutorialType Type { get; private set; }
        [field: SerializeField] public bool IgnoreTimeScale { get; private set; }
        [field: SerializeField] public bool IsStartByTrigger { get; private set; }
        [SerializeField] private List<TutorialPanel> _panels;

        private int _currentPanel = 0;

        private void TutorialPanel_OnHide()
        {
            _panels[_currentPanel].OnHide -= TutorialPanel_OnHide;
            _currentPanel++;

            if (_currentPanel >= _panels.Count)
                StopShow();
            else
                StartShow();
        }

        public void StartShow()
        {
            _panels[_currentPanel].OnHide += TutorialPanel_OnHide;
            _panels[_currentPanel].Show();
        }

        public void StopShow()
        {
            OnTutorialPointComplete?.Invoke(this);
        }
    }
}