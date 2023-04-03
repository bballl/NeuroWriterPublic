using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Attack;
using GameBoxProject;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MenuAndUI
{
    internal class LevelSelectionMenuController : MonoBehaviour
    {
        [SerializeField] private RectTransform _framePrefab;
        [SerializeField] private CharacterStatsShower _statsShower;
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private AudioSource _audioSourceClickSound;
        [SerializeField] private AudioSource _audioSourceExitSound;

        private SceneContent _selectedContent;
        private bool _weaponSelected = false;
        private RectTransform _frame;

        private void Start()
        {
            _startGameButton.interactable = false;
            ButtonLocker.OnLevelSelected += OnLevelSelected;
            ButtonLocker.OnWeaponSelected += OnWeaponSelected;

            _startGameButton.onClick.AddListener(LoadScene);
            
            _backButton.onClick.AddListener(() => Invoke("LoadMainMenu", 0.2f));
            
            _backButton.onClick.AddListener(_audioSourceExitSound.Play);
            _startGameButton.onClick.AddListener(_audioSourceClickSound.Play);
        }

        private void TryShowButton()
        {
            if (_weaponSelected && _selectedContent != null)
                _startGameButton.interactable = true;
        }

        private void OnLevelSelected(ButtonLocker levelIcon, SceneContent content)
        {
            _selectedContent = content;
            SetFrameToButton(levelIcon.transform);
            TryShowButton();
        }

        private void SetFrameToButton(Transform parent)
        {
            if (_frame == null)
                _frame = Instantiate(_framePrefab, parent);

            _frame.transform.SetParent(parent);
            _frame.anchoredPosition = Vector2.zero;
        }

        private void OnWeaponSelected()
        {
            _weaponSelected = true;
            TryShowButton();
        }

        private void LoadScene()
        {
            Instantiate(_selectedContent);
            SceneController.LoadSceneWithFade(Scenes.GameLevel);
        }

        private void LoadMainMenu()
        {
            SceneController.LoadScene(Scenes.MainMenu);
        }

        private void OnDestroy()
        {
            ButtonLocker.OnWeaponSelected -= OnWeaponSelected;
            ButtonLocker.OnLevelSelected -= OnLevelSelected;
            _startGameButton.onClick.RemoveListener(LoadScene);
            _backButton.onClick.RemoveListener(LoadMainMenu);
        }
    }
}