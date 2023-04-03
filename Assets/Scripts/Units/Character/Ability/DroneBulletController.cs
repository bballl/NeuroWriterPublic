using GameBoxProject;
using UnityEngine;

namespace Assets.Scripts.Units.Character.Ability
{
    internal class DroneBulletController : MonoBehaviour
    {
        [SerializeField] AbilityData _abilityData;
        private void Start()
        {
            float delay = 10f;
            Invoke("BulletDestroy", delay);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamagable target))
            {
                target.TakeDamage(_abilityData.DroneDamage);
                Destroy(gameObject);
            }
        }

        private void BulletDestroy()
        {
            Destroy(gameObject);
        }
    }
}
