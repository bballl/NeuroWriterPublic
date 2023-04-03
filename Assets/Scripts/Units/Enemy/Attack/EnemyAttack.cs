using UnityEngine;

namespace GameBoxProject
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        protected float _damage;

        public virtual void Init(float damage)
        {
            _damage = damage;
        }
    }
}