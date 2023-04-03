using TMPro;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class GoldUI : MonoBehaviour
    {
        [SerializeField] private GoldHolder _goldHolder;
        [SerializeField] private TMP_Text _goldText;
        [Range(1, 2)]
        [SerializeField] private float _scaleEffectMultiplier;

        private Vector2 _textStartScale;

        private void Start()
        {
            _textStartScale = _goldHolder.transform.localScale;

            _goldHolder.Gold.OnGoldCountChanged += OnGoldCountChanged;
            OnGoldCountChanged(_goldHolder.Gold.Current);
        }

        private void OnGoldCountChanged(int goldCount)
        {
            DOTween.Sequence()
                .Append(_goldText.transform.DOScale(_textStartScale * _scaleEffectMultiplier, 0.2f))
                .AppendCallback(() => _goldText.text = goldCount.ToString())
                .Append(_goldText.transform.DOScale(_textStartScale, 0.3f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void OnDestroy()
        {
            _goldHolder.Gold.OnGoldCountChanged -= OnGoldCountChanged;
        }
    }
}