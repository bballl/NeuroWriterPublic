using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.MenuAndUI;

namespace GameBoxProject
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        [SerializeField] private List<DialogScene> _dialogScenes;
        [SerializeField] private Image _backGroundImage;
        [SerializeField] private DialogPanel _dialogPanel;

        [SerializeField] private float _timeToHideImage;
        [SerializeField] private float _timeToShowImage;
        [SerializeField] private float _timeInBlack;

        private int _currentFrameIndex = 0;
        private int _currentReplicaIndex = 0;

        private void Start()
        {
            DialogPanel.OnReplicaShowed += OnReplicaShowed;
            _exitButton.onClick.AddListener(BackToMainMenu);

            DOTween.Sequence()
                .AppendCallback(() => _backGroundImage.sprite = _dialogScenes[_currentFrameIndex].BackImage)
                .Append(_backGroundImage.DOFade(1, _timeToShowImage))
                .AppendCallback(() => _dialogPanel.Activate(_dialogScenes[_currentFrameIndex].Dialogs[_currentReplicaIndex]))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void BackToMainMenu()
        {
            _dialogPanel.StopAllCoroutines();
            SceneController.LoadSceneWithFade(Scenes.MainMenu);
        }

        private void ChangeFrame()
        {
            DOTween.Sequence()
                .Append(_backGroundImage.DOFade(0, _timeToHideImage))
                .AppendCallback(() => _backGroundImage.sprite = _dialogScenes[_currentFrameIndex].BackImage)
                .AppendInterval(_timeInBlack)
                .Append(_backGroundImage.DOFade(1, _timeToShowImage))
                .AppendCallback(() => _dialogPanel.Activate(_dialogScenes[_currentFrameIndex].Dialogs[_currentReplicaIndex]))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void OnReplicaShowed(DialogData obj)
        {
            _currentReplicaIndex++;

            if (_currentReplicaIndex < _dialogScenes[_currentFrameIndex].Dialogs.Count)
            {
                _dialogPanel.Activate(_dialogScenes[_currentFrameIndex].Dialogs[_currentReplicaIndex]);
            }
            else
            {
                _currentFrameIndex++;
                _currentReplicaIndex = 0;

                if (_currentFrameIndex < _dialogScenes.Count)
                {
                    ChangeFrame();
                }
                else
                {
                    BackToMainMenu();
                }
            }
        }

        private void OnDestroy()
        {
            DialogPanel.OnReplicaShowed -= OnReplicaShowed;
        }
    }
}