using Assets.Scripts.Units.Character;
using GameBoxProject;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MenuAndUI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _intermediateMenuPanel;
        [SerializeField] private GameObject _aboutAuthorsPanel;

        [SerializeField] private Button _prologButton;
        [SerializeField] private Button _gameButton;
        [SerializeField] private Button _aboutAuthorsButton;
        [SerializeField] private Button _settingsButton; //отключиться

        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _levelSelectionButton;
        [SerializeField] private Button _diariesButton;

        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private AudioSource _audioSourceClickSound;
        [SerializeField] private AudioSource _audioSourceExitSound;

        private void Awake()
        {
            ClickSoundSubscription();

            _aboutAuthorsPanel.SetActive(false);
            _intermediateMenuPanel.SetActive(false);
            _backButton.gameObject.SetActive(false);
            _diariesButton.gameObject.SetActive(false); //пока не готовы дневники

            _prologButton.onClick.AddListener(() => SceneController.LoadSceneWithFade(Scenes.PrologueScene));

            _aboutAuthorsButton.onClick.AddListener(() => OpenAboutAuthorsPanel());
            _settingsButton.onClick.AddListener(() => StartCoroutine(LoadSceneViaPause(Scenes.SettingsScene)));   //SceneController.LoadScene(Scenes.SettingsScene));
            
            _shopButton.onClick.AddListener(() => StartCoroutine(LoadSceneViaPause(Scenes.ShopScene)));           //SceneController.LoadScene(Scenes.ShopScene));

            _gameButton.onClick.AddListener(() => OpenIntermediateMenuPanel());
            //_gameButton.onClick.AddListener(() => LoadScene((int)Scenes.GameLevel));
            
            _levelSelectionButton.onClick.AddListener(() => StartCoroutine(LoadSceneViaPause(Scenes.LevelSelectionScene))); //SceneController.LoadScene(Scenes.LevelSelectionScene));
            //_diariesButton.onClick.AddListener(() => LoadScene((int)Scenes.DiariesScene));


            _exitButton.onClick.AddListener(() => Invoke("Exit", 0.2f));
            _backButton.onClick.AddListener(() => BackToMainMenu());
        }

        private IEnumerator LoadSceneViaPause(Scenes sceneName)
        {
            yield return new WaitForSeconds(0.2f);
            
            switch (sceneName)
            {
                case Scenes.SettingsScene:
                    SceneController.LoadScene(Scenes.SettingsScene);
                    break;
                case Scenes.ShopScene:
                    SceneController.LoadScene(Scenes.ShopScene);
                    break;
                case Scenes.LevelSelectionScene:
                    SceneController.LoadScene(Scenes.LevelSelectionScene);
                    break;
                default:
                    Debug.Log("Ошибка загрузки сцены");
                    break;
            }
        }

        /// <summary>
        /// Показать текст, описывающий соответствующий раздел меню.
        /// </summary>
        private void OpenAboutAuthorsPanel()
        {
            _mainMenuPanel.SetActive(false);
            _intermediateMenuPanel.SetActive(false);
            _aboutAuthorsPanel.SetActive(true);
            _backButton.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Открыть панель промежуточного меню.
        /// </summary>
        private void OpenIntermediateMenuPanel()
        {
            _intermediateMenuPanel.SetActive(true);
            _aboutAuthorsPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _backButton.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Возврат в главное меню.
        /// </summary>
        private void BackToMainMenu()
        {
            _intermediateMenuPanel.SetActive(false);
            _aboutAuthorsPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
            _backButton.gameObject.SetActive(false);
        }

        private void ClickSoundSubscription()
        {
            _prologButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            _gameButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            _aboutAuthorsButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            _settingsButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            _shopButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            _levelSelectionButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            _diariesButton.onClick.AddListener(() => _audioSourceClickSound.Play());
            
            _exitButton.onClick.AddListener(() => _audioSourceExitSound.Play());
            _backButton.onClick.AddListener(() => _audioSourceExitSound.Play());
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}

