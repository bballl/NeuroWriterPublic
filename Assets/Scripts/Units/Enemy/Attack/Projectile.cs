using System;
using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    class Projectile : MonoBehaviour, IPoolObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _timeToDestroy;

        public event Action<IPoolObject> OnObjectNeededToDeactivate;

        private float _damage;

        public void Init(float damage)
        {
            transform.SetParent(null);
            _damage = damage;
            StartCoroutine(WaitToDestroy());
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(_timeToDestroy);
            OnObjectNeededToDeactivate?.Invoke(this);
        }

        public void ResetBeforeBackToPool()
        {
            gameObject.SetActive(false);
            _rigidbody2D.velocity = Vector2.zero;
            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.identity;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damage);
                StopAllCoroutines();
                OnObjectNeededToDeactivate?.Invoke(this);
            }
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = transform.up * _speed;
        }
    }
}
