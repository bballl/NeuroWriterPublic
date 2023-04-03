using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.MenuAndUI;

namespace GameBoxProject
{
    public class EndGamePanels : MonoBehaviour
    {
        [SerializeField] private UIPanel _deathPanel;
        [SerializeField] private UIPanel _winPanel;

        [SerializeField] private Button _nextSceneButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        [SerializeField] private MainCharacterController _mainCharacter;

        private bool _isReady;

        private void Awake() => 
            StartCoroutine(WaitRoutine());

        private IEnumerator WaitRoutine()
        {
            while (_isReady is false)
            {
                if (_mainCharacter == null)
                {
                    Debug.Log($"{name} waiting for Character");
                    yield return new WaitForFixedUpdate();
                }
                else if (_mainCharacter.Health == null)
                {
                    Debug.Log($"{name} waiting for Health in Character");
                    yield return new WaitForFixedUpdate();
                }

                _isReady = true;
            }

            Construct();
            yield break;
        }

        private void Construct()
        {
            Timer.OnTimeEnded += WinGame;
            _mainCharacter.Health.OnPersonDead += LoseGame;

            _nextSceneButton.onClick.AddListener(LoadNext);
            _restartButton.onClick.AddListener(Restart);
            _mainMenuButton.onClick.AddListener(LoadMenu);
        }

        private void LoseGame(object character)
        {
            DOTween.Sequence()
                .AppendCallback(() => Time.timeScale = 0.1f)
                .AppendInterval(1f)
                .AppendCallback(() => _deathPanel.Show())
                .AppendCallback(() => Time.timeScale = 0f)
                .SetUpdate(true)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable)
                .Play();
        }

        private void WinGame()
        {
            DOTween.Sequence()
                .AppendCallback(() => Time.timeScale = 0.1f)
                .AppendInterval(1f)
                .AppendCallback(() => _winPanel.Show())
                .AppendCallback(() => Time.timeScale = 0f)
                .SetUpdate(true)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable)
                .Play();            
        }

        private void LoadNext()
        {
            SceneController.LoadSceneWithFade(Scenes.WordsScene);
        }

        private void Restart()
        {
            SceneController.LoadSceneWithFade(Scenes.GameLevel);
        }

        private void LoadMenu()
        {
            SceneController.LoadSceneWithFade(Scenes.MainMenu);
        }

        private void OnDestroy()
        {
            Timer.OnTimeEnded -= WinGame;
            _mainCharacter.Health.OnPersonDead -= LoseGame;
        }
    }
}