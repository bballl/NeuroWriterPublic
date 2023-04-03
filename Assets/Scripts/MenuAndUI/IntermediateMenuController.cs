using GameBoxProject;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MenuAndUI
{
    internal class IntermediateMenuController : MonoBehaviour
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _levelSelectionButton;
        [SerializeField] private Button _diariesButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            _backButton.onClick.AddListener(() => LoadScene(Scenes.MainMenu));
            _shopButton.onClick.AddListener(() => LoadScene(Scenes.ShopScene));
            _levelSelectionButton.onClick.AddListener(() => LoadScene(Scenes.LevelSelectionScene));
            _diariesButton.onClick.AddListener(() => LoadScene(Scenes.DiariesScene));
        }

        private void LoadScene(Scenes scene)
        {
            _audioSource.Play();
            SceneController.LoadScene(scene);
        }
    }
}
