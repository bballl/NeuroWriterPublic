using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace GameBoxProject
{
    public class PropWithItem : MonoBehaviour, IDamagable
    {
        [SerializeField] private SpriteRenderer _buildSpriteRend;
        [SerializeField] private SpriteRenderer _brokerSpriteRend;
        [SerializeField] private SpriteRenderer _shadow;

        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _sound;

        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Animator _animator;

        public float CurrentHP => _health.Hp;

        private Health _health;
        private DataServerData _data;
        private Vector3 _startScale;

        private void Awake()
        {
            _startScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        public void Init(DataServerData data)
        {
            _data = data;
            _collider2D.enabled = true;

            _health = new Health(data.Health);
            _health.OnPersonDead += DestroyServer;

            _buildSpriteRend.gameObject.SetActive(true);
            _shadow.gameObject.SetActive(true);
            _brokerSpriteRend.gameObject.SetActive(false);

            DOTween.Sequence()
                .Append(transform.DOScale(1.1f * _startScale, 0.5f))
                .Append(transform.DOScale(_startScale, 0.2f))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void DestroyServer(object obj)
        {
            _buildSpriteRend.gameObject.SetActive(false);
            _shadow.gameObject.SetActive(false);
            _brokerSpriteRend.gameObject.SetActive(true);
            _animator.enabled = false;
            _collider2D.enabled = false;
            //CameraNoiser.Shake(ShakeType.Medium, 1f);

            DOTween.Sequence()
                .AppendCallback(() => DropItem())
                .AppendInterval(1f)
                .Append(_brokerSpriteRend.DOFade(0, 2f))
                .AppendCallback(() => Destroy(gameObject))
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void DropItem()
        {
            float summ = 0;

            if (_data.ChanceToDropGold + _data.ChanceToDropHealth >= 1)
            {
                summ = _data.ChanceToDropGold + _data.ChanceToDropHealth;
            }
            else
                summ = 1f;

            float res = Random.value * summ;

            if (res <= _data.ChanceToDropHealth)
            {
                DropController.CreateDrop<DroppedHp>(_data.HealthDropCount, transform.position);
            }
            else if (res <= (_data.ChanceToDropHealth + _data.ChanceToDropGold))
            {
                DropController.CreateDrop<DroppedGold>(_data.GoldDropCount, transform.position);
            }
        }

        public void TakeDamage(float damage)
        {
            if (_health.IsAlive)
            {
                ShowDamage();
                //CameraNoiser.Shake(ShakeType.Low);
                _audio.PlayOneShot(_sound);
                _health.TakeDamage(damage);
            }
        }

        public void ShowDamage()
        {
            DOTween.Sequence()
                .Append(_buildSpriteRend.DOColor(Color.red, 0.1f))
                .Append(transform.DOScale(_startScale * 1.1f, 0.1f))
                .Append(transform.DOScale(_startScale, 0.1f))
                .Append(_buildSpriteRend.DOColor(Color.white, 0.5f))
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private void OnDestroy()
        {
            _health.OnPersonDead -= DestroyServer;
        }
    }
}