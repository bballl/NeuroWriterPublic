using Assets.Scripts.MenuAndUI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _settingsPanel;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _backToGameButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _goToMainMenuButton;
        [SerializeField] private Button _backFromSettingsButton;

        private bool _inPause = false;
        private float _timeScaleBeforePause;

        private void Start()
        {
            _pausePanel.SetActive(false);
            _settingsPanel.SetActive(false);

            _pauseButton.onClick.AddListener(PauseGame);
            _backToGameButton.onClick.AddListener(UnPauseGame);

            _restartButton.onClick.AddListener(RestartGame);

            _settingsButton.onClick.AddListener(OpenSettings);
            _backFromSettingsButton.onClick.AddListener(CloseSettings);

            _goToMainMenuButton.onClick.AddListener(LoadMainMenu);
        }

        private void LoadMainMenu()
        {
            SceneController.LoadSceneWithFade(Scenes.MainMenu);
        }

        private void PauseGame()
        {
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0f;
            _pausePanel.SetActive(true);
            _inPause = true;
        }

        private void UnPauseGame()
        {
            Time.timeScale = _timeScaleBeforePause;
            _pausePanel.SetActive(false);
            _inPause = false;
        }

        private void OpenSettings()
        {
            _settingsPanel.SetActive(true);
        }

        private void CloseSettings()
        {
            _settingsPanel.SetActive(false);
        }

        private void RestartGame()
        {
            SceneController.LoadSceneWithFade(Scenes.GameLevel);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_settingsPanel.activeInHierarchy)
                {
                    CloseSettings();
                    return;
                }

                if (_inPause)
                    UnPauseGame();
                else
                    PauseGame();
            }
        }
    }
}