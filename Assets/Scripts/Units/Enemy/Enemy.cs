using System;
using UnityEngine;

namespace GameBoxProject
{
    public abstract class Enemy : MonoBehaviour, IDamagable, IPoolObject
    {
        protected static TextRendererParticleSystem _textRendererParticleSystem;
        public static event Action OnEnemyDead;

        public event Action<IPoolObject> OnObjectNeededToDeactivate;
       
        [SerializeField] protected EnemyMovement _enemyMovement;
        [SerializeField] protected EnemyAttack _enemyAttack;
        [SerializeField] protected EnemyModel _enemyModel;
        [SerializeField] protected Collider2D _bodyCollider;

        public Health Health { get; protected set; }

        public float CurrentHP => Health.Hp;

        protected EnemyData _data;
        protected Letter _letterData;

        public void ResetBeforeBackToPool()
        {
            
        }

        public virtual void TakeDamage(float damage)
        {
            if (Health.IsAlive is false)
                return;

            float lastHp = Health.Hp;

            float givenDamage = (lastHp > damage) ? damage : lastHp;

            if (givenDamage != 0)
            {
                float greenColor = (255f - Mathf.Clamp(givenDamage, 0f, 255f))/255f;
                Color color = new Color(1f, greenColor, 0f);
                _textRendererParticleSystem.SpawnParticle(transform.position, givenDamage, color, 0.8f);
            }

            Health.TakeDamage(givenDamage);

            if (Health.IsAlive)
                _enemyModel.ShowDamage();
        }

        internal virtual void Init(EnemyData data, CommonEnemyData dataByMinute)
        {
            if (_textRendererParticleSystem == null)
                _textRendererParticleSystem = FindObjectOfType<TextRendererParticleSystem>();

            _data = data;
            _enemyAttack.Init(dataByMinute.Damage);

            Health = new Health(dataByMinute.HP);

            if (_bodyCollider != null)
                _bodyCollider.enabled = true;

            Health.OnPersonDead += Dead;

            _enemyMovement.Construct(data);
        }

        public void InitModel(Letter data)
        {
            _letterData = data;
            _enemyModel.Construct(data);
        }

        public virtual void SetTargetToFollow(Transform target)
        {
            _enemyMovement?.SetTarget(target);
        }

        protected virtual void Dead(object obj)
        {
            if (_bodyCollider != null)
                _bodyCollider.enabled = false;
            Health.OnPersonDead -= Dead;
            //CameraNoiser.Shake(ShakeType.Low);

            _enemyMovement.StopMove();
            _enemyModel.PlayDeath();
            _enemyModel.OnDeadAnimationEnded += OnDead;
        }

        private void OnDead()
        {
            _enemyModel.OnDeadAnimationEnded -= OnDead;
            OnEnemyDead?.Invoke();
            gameObject.SetActive(false);
            OnObjectNeededToDeactivate?.Invoke(this);
        }
    }
}