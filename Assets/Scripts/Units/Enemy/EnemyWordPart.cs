using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public class EnemyWordPart : MonoBehaviour, IPoolObject, IDamagable
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAttack _enemyPartAttack;
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private Collider2D _collider;

        public event Action<float> OnDamageTaken;

        public Health Health;

        protected Letter _letterData;

        public float CurrentHP => Health.Hp;

        public event Action<IPoolObject> OnObjectNeededToDeactivate;

        public void InitModel(Letter letter)
        {
            _letterData = letter;
            _enemyModel.Construct(letter);
        }

        public void ShowTakeDamage()
        {
            if (Health.IsAlive)
                _enemyModel.ShowDamage();
        }

        public void Init(EnemyData data, CommonEnemyData dataByMinute, Health health)
        {
            if (_collider != null)
                _collider.enabled = true;

            Health = health;
            _enemyMovement.Construct(data);
            _enemyPartAttack.Init(dataByMinute.Damage);
        }

        public void SetTargetToFollow(Transform target)
        {
            _enemyMovement.SetTarget(target);
        }

        public void ResetBeforeBackToPool()
        {
            
        }

        public void Dead()
        {
            if (_collider != null)
                _collider.enabled = false;

            _enemyModel.PlayDeath();
            _enemyMovement.StopMove();
        }

        public void BackToPool()
        {
            gameObject.SetActive(false);
            OnObjectNeededToDeactivate?.Invoke(this);
        }

        public void TakeDamage(float damage)
        {
            OnDamageTaken?.Invoke(damage);
        }
    }
}