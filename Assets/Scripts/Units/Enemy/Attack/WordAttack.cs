using System;
using System.Collections;
using UnityEngine;

namespace GameBoxProject
{
    public class WordAttack : EnemyAttack
    {
        public static event Action OnWordStartAttack;

        [SerializeField] private WordAttackData _data;
        [SerializeField] private Projectile _projectile;

        private WaitForSeconds _delay;
        private Transform _attackTransform;
        private Pool<Projectile> _pool;

        public override void Init(float damage)
        {
            _pool = new Pool<Projectile>(_data.CountOfProjectiles, _projectile, true);
            base.Init(damage);
            _delay = new WaitForSeconds(_data.TimeBetweenAttack);
            StartCoroutine(PrepareToAttack());
        }

        public void SetHead(Transform head)
        {
            _attackTransform = head;
        }

        public IEnumerator PrepareToAttack()
        {   
            while (true)
            {
                yield return _delay;
                OnWordStartAttack?.Invoke();
                yield return new WaitForSeconds(0.4f);
                Attack();
            }
        }

        public void Attack()
        {
            float angle = 360f / (float)_data.CountOfProjectiles;

            for (int i = 0; i < _data.CountOfProjectiles; i++)
            {
                var bullet = _pool.GetObject();
                bullet.Init(_damage);
                bullet.transform.localPosition = _attackTransform.position;
                bullet.transform.localRotation = Quaternion.Euler(0f, 0f, angle * i);
            }
        }
    }
}