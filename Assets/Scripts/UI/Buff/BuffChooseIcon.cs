using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace GameBoxProject
{
    class BuffChooseIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Buff> OnBuffChoosen;

        [SerializeField] private Image _icon;
        [SerializeField] private Outline _outline;
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rTransform;

        private TMP_Text _descriptionText;
        private Buff _buff;

        public void Activate(Buff buff, TMP_Text textHolder)
        {
            _outline.enabled = false;
            _buff = buff;
            _descriptionText = textHolder;

            _icon.sprite = _buff.Data.Icon;

            _button.onClick.AddListener(ChooseBuff);
        }

        private void ChooseBuff()
        {
            _button.onClick.RemoveListener(ChooseBuff);
            OnBuffChoosen?.Invoke(_buff);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _outline.enabled = true;
            _descriptionText.text = _buff.GetDescription();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outline.enabled = false;
            _descriptionText.text = string.Empty;
        }
    }
}
