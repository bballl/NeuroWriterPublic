using GameBoxProject;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Units.Character.Ability
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] AbilityData _abilityData;
        [SerializeField] LayerMask _layerMask;

        private GameObject _droneBullet;
        private Coroutine _coroutine;
        private int _bulletSpeed = 3;

        private void Awake()
        {
            _droneBullet = Resources.Load<GameObject>("DroneBullet");
        }

        private void Start()
        {
            _coroutine = StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(_abilityData.DroneRechargeTime);
            TargetSelection();
            
            _coroutine = null;
            _coroutine = StartCoroutine(Attack());
        }
        
        private void TargetSelection()
        {
            var collider = Physics2D.OverlapCircle(transform.position, _abilityData.DroneAttackRadius, _layerMask);
            if (collider == null)
                return;

            var bullet = GameObject.Instantiate(_droneBullet, transform.position, transform.rotation);
            var direction = ((Vector2)collider.transform.position - (Vector2)transform.position).normalized;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
