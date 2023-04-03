using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class MouseObserver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public static event Action OnMouseEnter;
        public static event Action OnButtonClicked;

        [SerializeField] private GameObject _frame;
        [SerializeField] private TMP_Text _text;

        private Color _startColor;
        private Button _button;

        private void Start()
        {
            TryGetComponent(out _button);

            if (_text != null)
               _startColor = _text.color;
            _frame?.SetActive(false);

            if (_button != null)
                _button.onClick.AddListener(() => OnButtonClicked?.Invoke());
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_button.interactable is false)
                return;

            _frame?.SetActive(true);
            if (_text != null)
                _text.color = Color.white;
            OnMouseEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_button.interactable is false)
                return;

            _frame?.SetActive(false);
            if (_text != null)
                _text.color = _startColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _frame?.SetActive(false);

            if (_button == null)
                OnButtonClicked?.Invoke();
        }
    }
}