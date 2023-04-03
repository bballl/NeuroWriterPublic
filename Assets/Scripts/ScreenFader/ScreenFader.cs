using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameBoxProject
{
    public class ScreenFader : MonoBehaviour
    {
        public static ScreenFader Instance { get; private set; }

        [SerializeField] private Image _blackScreenImage;

        [SerializeField] private float _timeToFadeImage;
        [SerializeField] private float _timeToFadeOutImage;
        [SerializeField] private float _timeInFade;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                FadeOut();
            }
            else
                Destroy(gameObject);
        }

        public void Activate(TweenCallback callback)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() => SetImageActive(true))
                .Append(_blackScreenImage.DOFade(1, _timeToFadeImage))
                .AppendCallback(callback)
                .AppendInterval(_timeInFade)
                .Append(_blackScreenImage.DOFade(0, _timeToFadeOutImage))
                .AppendCallback(() => SetImageActive(false))
                .SetUpdate(true)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void SetImageActive(bool state) =>
            _blackScreenImage.gameObject.SetActive(state);

        private void FadeOut()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() => SetImageActive(true))
                .AppendInterval(_timeInFade)
                .Append(_blackScreenImage.DOFade(0, _timeToFadeOutImage))
                .AppendCallback(() => SetImageActive(false))
                .SetUpdate(UpdateType.Normal, true)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}