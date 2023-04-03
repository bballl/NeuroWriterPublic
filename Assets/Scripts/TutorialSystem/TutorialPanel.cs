using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameBoxProject
{
    public class TutorialPanel : MonoBehaviour
    {
        public event Action OnHide; 

        [SerializeField] private HelpMessage _helpMessagePrefab;
        [SerializeField] private Transform _messageParent;
        [Multiline(5)]
        [SerializeField] private string _message;

        private Button _nextButton;

        public void Show()
        {
            var help = Instantiate(_helpMessagePrefab, _messageParent);

            _nextButton = help.NextButton;
            _nextButton.onClick.AddListener(Hide);

            help.Construct(_message);

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _nextButton.onClick.RemoveListener(Hide);
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
    }
}