using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MenuAndUI
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private AudioSource _audioSourceExitSound;

        private void Awake()
        {
            _backButton.onClick.AddListener(() => BackToMainMenu());
            _backButton.onClick.AddListener(() => _audioSourceExitSound.Play());
        }

        private void BackToMainMenu()
        {
            Invoke("LoadMainMenuScene", 0.2f);
        }

        private void LoadMainMenuScene()
        {
            SceneManager.LoadScene((int)Scenes.MainMenu);
        }
    }
}
