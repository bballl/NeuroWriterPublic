using Assets.Scripts.Sounds;
using GameBoxProject;
using System.Collections;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.Units.Character.Attack
{
    public class PenController : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _particleSystemPosition;

        private PenSoundController _soundController;
        private ParticleSystem _shotParticleSystem;
        private ParticleSystem _penAttackParticleSystem;
        private Coroutine _coroutine;
        private int _targetsNumber;

        public int TargetNumber => _targetsNumber;

        private Vector3 _gizmosSphereCenter;

        private void Awake()
        {
            _shotParticleSystem = Resources.Load<ParticleSystem>("PenShotParticleSystem");
            _penAttackParticleSystem = Resources.Load<ParticleSystem>("PenAttackParticleSystem");
            _soundController = new PenSoundController(transform);
        }

        private void Start()
        {
            _targetsNumber = CharacterAttributes.PenTargetsNumber;
            
            _coroutine = StartCoroutine(Attack());

            Observer.IncreasePenTargetsNumberEvent += IncreaseTargetsNumber;
            
            Debug.Log($"Значение MinDamage на старте: {CharacterAttributes.PenMinDamage}");
            Debug.Log($"Значение MaxDamage на старте: {CharacterAttributes.PenMaxDamage}");
            Debug.Log($"Значение времени перезарядки на старте: {CharacterAttributes.PenRechargeTime}");
        }

        private void OnDestroy()
        {
            Observer.IncreasePenTargetsNumberEvent -= IncreaseTargetsNumber;
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(CharacterAttributes.PenRechargeTime);
            Shot();
            
            _coroutine = null;
            _coroutine = StartCoroutine(Attack());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawSphere(_gizmosSphereCenter, CharacterAttributes.PenAttackRadius);
        }

        private void Shot()
        {
            _soundController.Shot();
            ActivateShotParticleSystem();

            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, transform.right, CharacterAttributes.PenAttackDistance, _layerMask);
            
            if (hit != false)
            {
                ActivateInkParticleSystem(hit.transform.position);
                
                _gizmosSphereCenter = hit.transform.position;

                Collider2D[] results = new Collider2D[_targetsNumber];
                var targets = Physics2D.OverlapCircleNonAlloc(hit.collider.transform.position,
                    CharacterAttributes.PenAttackRadius, results, _layerMask);

                int index = 0;
                foreach (var collider in results)
                {
                    if (collider != null)
                    {
                        int damage = UnityEngine.Random.Range(CharacterAttributes.PenMinDamage, CharacterAttributes.PenMaxDamage);
                        collider.gameObject.TryGetComponent(out IDamagable damagable);
                        damagable.TakeDamage(damage);
                        _soundController.Hit();
                    }

                    index++;
                    if (index == _targetsNumber)
                        return;
                }
            }

            //старый вариант нанесения урона
            //if (hit.collider.gameObject.TryGetComponent(out IDamagable damagable))
            //{


            //    int damage = UnityEngine.Random.Range(CharacterAttributes.PenMinDamage, CharacterAttributes.PenMaxDamage);
            //    damagable.TakeDamage(damage);
            //    _soundController.Hit();
            //}
        }

        private void IncreaseTargetsNumber(int value)
        {
            _targetsNumber += value;
            //сюда дописать событие
            Debug.Log($"Увеличено количество целей у ручки. Текущее значение = {_targetsNumber}");
        }

        private void ActivateShotParticleSystem()
        {
            var ps = Instantiate(_shotParticleSystem, _particleSystemPosition);
            //ps.Play();
        }

        private void ActivateInkParticleSystem(Vector3 position) 
        {
            var ps = Instantiate(_penAttackParticleSystem, position, Quaternion.identity);
            //ps.Play();
        }
    }
}
