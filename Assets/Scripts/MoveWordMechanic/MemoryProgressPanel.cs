using Assets.Scripts.MenuAndUI;
using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Units.Character;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class MemoryProgressPanel : MonoBehaviour
    {
        public static event Action OnRoundComplete;

        [SerializeField] private TMP_Text _percentText;
        [SerializeField] private TMP_Text _needPercentToWinText;
        [SerializeField] private TMP_Text _rewardText;
        [SerializeField] private Image _filler;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _fillerSound;
        [SerializeField] private AudioClip _badSound;
        [SerializeField] private AudioClip _fullFillerSound;
        [SerializeField] private RectTransform _window;
        [SerializeField] private RectTransform _nextLevelNotification;
        [SerializeField] private Button _goToMenuButton;

        private bool _go;
        private RoundReward _roundReward;
        private int _percentTarget;
        private int _index = 0;
        private int _reward;

        public void Show(float fillerValue)
        {
            _goToMenuButton.onClick.AddListener(LoadSelectionMenu);
            _goToMenuButton.interactable = false;
            gameObject.SetActive(true);
            ShowProgress(fillerValue);
        }

        private void ShowProgress(float fillerValue)
        {
            _roundReward = SceneContent.Instance._rewardData;
            _percentTarget = _roundReward.GoldByProgresses[_index].MinPercent;

            if (fillerValue <= 0.01f)
            {
                _audioSource.PlayOneShot(_badSound);
                _goToMenuButton.interactable = true;
            }
            else
            {
                _needPercentToWinText.text = $"Ìèíèìàëüíûé ïðîöåíò äëÿ îòêðûòèÿ ñëåäóþùåãî ðàóíäà - {_roundReward.MinValueToCompleteRound}%";

                Sequence seq = DOTween.Sequence();

                seq.AppendInterval(1f)
                    .AppendCallback(() => _go = true)
                    .AppendCallback(() => _audioSource.PlayOneShot(_fillerSound))
                    .Append(_filler.DOFillAmount(fillerValue, 3f)).SetEase(Ease.OutQuad)
                    .AppendCallback(() => _go = false)
                    .AppendCallback(() => _audioSource.Stop())
                    .AppendCallback(() =>
                    {
                        if (_filler.fillAmount * 100f > _roundReward.MinValueToCompleteRound)
                        {
                            int currentGameLevel = new LoadData().GetIntData(GlobalVariables.Level);

                            if (SceneContent.Instance.LevelIndex == currentGameLevel)
                            {
                                new SaveData(GlobalVariables.Level, currentGameLevel + 1);
                                ShowOpenRoundText();
                            }
                            OnRoundComplete?.Invoke();
                        }

                        if (_filler.fillAmount > 0.99f)
                            _audioSource.PlayOneShot(_fullFillerSound);
                    })
                    .AppendCallback(() => _goToMenuButton.interactable = true)
                    .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
            }
        }

        private void ShowOpenRoundText()
        {
            DOTween.Sequence()
                .Append(_nextLevelNotification.DOAnchorPosY(-50, 0.5f)).SetEase(Ease.OutCubic)
                .AppendInterval(3f)
                .Append(_nextLevelNotification.DOAnchorPosY(50, 2f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void Update()
        {
            if (_go)
            {
                _percentText.text = $"{_filler.fillAmount.ToString("##.#%")}";
                if (_filler.fillAmount * 100f >= _percentTarget)
                {
                    _reward = _roundReward.GoldByProgresses[_index].GoldCount;
                    _index++;

                    if (_roundReward.GoldByProgresses.Count <= _index)
                        _percentTarget = 200;
                    else
                        _percentTarget = _roundReward.GoldByProgresses[_index].MinPercent;

                    ShowMoneyEffect();
                }
            }
        }

        private void ShowMoneyEffect()
        {
            //ÄÎÁÀÂÈÒÜ ÀÍÈÌÀÖÈÉ
            _rewardText.text = $"ÍÀÃÐÀÄÀ : {_reward}";
        }

        public void LoadSelectionMenu()
        {
            _goToMenuButton.onClick.RemoveListener(LoadSelectionMenu);

            if (_reward != 0)
                Observer.SaveCoinsEvent(_reward);

            SceneController.LoadSceneWithFade(Scenes.LevelSelectionScene);
        }
    }
}