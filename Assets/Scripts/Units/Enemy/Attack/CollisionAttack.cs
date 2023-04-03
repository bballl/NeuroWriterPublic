using UnityEngine;

namespace GameBoxProject
{
    class CollisionAttack : EnemyAttack
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damage);
            }
        }
    }
}