using UnityEngine;
using Assets.Scripts.SaveLoadData;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

namespace GameBoxProject
{
    public class ButtonLocker : MonoBehaviour
    {
        public static event Action<ButtonLocker, SceneContent> OnLevelSelected;
        public static event Action OnWeaponSelected;

        [SerializeField] private int _levelNeedToOpen;

        [SerializeField] private Button _button;
        [SerializeField] private Image _lockImage;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;

        [SerializeField] private BlockButtonIcons _data;

        private void Start()
        {
            _icon.sprite = _data.Icon;
            _nameText.text = _data.Name;

            _button.onClick.AddListener(SelectLevel);

            Refresh();
        }

        private void SelectLevel()
        {
            if (_data.IconType == IconType.Round)
                OnLevelSelected?.Invoke(this, _data.ContentPrefab);
            else if (_data.IconType == IconType.Weapon)
                OnWeaponSelected?.Invoke();
        }

        public void Refresh()
        {
            if (_levelNeedToOpen <= new LoadData().GetIntData(GlobalVariables.Level))
            {
                _button.interactable = true;
                _lockImage.DOFade(0, 0.5f);
            }
            else
            {
                _button.interactable = false;
                _lockImage.DOFade(1, 0.3f);
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SelectLevel);
        }
    }
}