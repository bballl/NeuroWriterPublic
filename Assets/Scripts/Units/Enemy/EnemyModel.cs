using System;
using UnityEngine;
using DG.Tweening;

namespace GameBoxProject
{
    public class EnemyModel : MonoBehaviour
    {
        public event Action OnDeadAnimationEnded;

        private static readonly int DeathTrigger = Animator.StringToHash("Death");
        private static readonly int BornTrigger = Animator.StringToHash("Born");

        [SerializeField] private SpriteRenderer _modelSprite;
        [SerializeField] private SpriteRenderer _symbolIcon;
        [SerializeField] private Animator _animator;

        private Sequence _damageSeq;

        public Letter LetterData { get; private set; }

        private Vector3 _startIconScale;
        private Vector3 _startModelScale;

        private void Awake()
        {
            _startModelScale = transform.localScale;
            _startIconScale = _symbolIcon.transform.localScale;
        }

        public void Construct(Letter data)
        {
            _animator.SetTrigger(BornTrigger);

            _symbolIcon.DOFade(1f, 0.5f)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);

            LetterData = data;
            _symbolIcon.sprite = LetterData.RightIcon;
            _modelSprite.color = Color.white;
        }

        public void LookAtRight()
        {
            _symbolIcon.transform.localScale = _startIconScale;
            _symbolIcon.sprite = LetterData.RightIcon;
        }

        public void LookAtLeft()
        {
            if (!LetterData.IsSimetric)
            {
                _symbolIcon.transform.localScale = new Vector3(-_startIconScale.x, _startIconScale.y, _startIconScale.z);
                _symbolIcon.sprite = LetterData.LeftIcon;
            }
        }

        public void ShowDamage()
        {
            _damageSeq = DOTween.Sequence()
                .Append(_modelSprite.DOColor(Color.red, 0.1f))
                .Append(transform.DOScale(_startModelScale * 1.1f, 0.1f))
                .Append(transform.DOScale(_startModelScale, 0.1f))
                .Append(_modelSprite.DOColor(Color.white, 0.5f))
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        public void PlayDeath()
        {
            _damageSeq?.Kill();

            _modelSprite.color = Color.white;
            transform.localScale = _startModelScale;

            _symbolIcon.DOFade(0f, 0.5f)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);

            DOTween.Sequence()
                .AppendCallback(() => _animator.SetTrigger(DeathTrigger))
                .AppendInterval(2f)
                .AppendCallback(() => OnDeadAnimationEnded?.Invoke())
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}