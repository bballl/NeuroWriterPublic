using System;
using GameBoxProject;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Units.Character.Attack
{
    public class PencilController : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        private PencilSoundController _soundController;
        private Transform _transformStartPosition;
        private Coroutine _coroutineAttack;
        private int _targetsNumber;

        public int TargetNumber => _targetsNumber;

        private void Awake()
        {
            _soundController = new PencilSoundController(transform);
        }
        private void Start()
        {
            _targetsNumber = CharacterAttributes.PencilTargetsNumber;
            _transformStartPosition = GameObject.FindGameObjectWithTag("CharacterWeapon").transform; //переделать
            _coroutineAttack = StartCoroutine(ImpulseAttack());

            Observer.IncreasePencilTargetsNumberEvent += IncreaseTargetsNumber;

            Debug.Log($"Значение MinDamage на старте: {CharacterAttributes.PencilMinDamage}");
            Debug.Log($"Значение MaxDamage на старте: {CharacterAttributes.PencilMaxDamage}");
            Debug.Log($"Значение времени перезарядки на старте: {CharacterAttributes.PencilRechargeTime}");
            Debug.Log($"Значение количества целей на старте: {CharacterAttributes.PencilTargetsNumber}");
        }

        private void OnDestroy()
        {
            Observer.IncreasePencilTargetsNumberEvent -= IncreaseTargetsNumber;
        }

        private IEnumerator ImpulseAttack()
        {
            yield return new WaitForSeconds(CharacterAttributes.PencilRechargeTime);
            ImpulsePoke();
            yield return new WaitForSeconds(0.05f);
            Return();

            _coroutineAttack = null;
            _coroutineAttack = StartCoroutine(ImpulseAttack());
        }

        private void ImpulsePoke()
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(transform.position, transform.right, CharacterAttributes.PencilAttackDistance, _layerMask);

            Debug.DrawRay(transform.position, transform.right * CharacterAttributes.PencilAttackDistance, Color.red, 0.1f);

            if (hits.Length > 0 )        //воспроизведение звука
                _soundController.Hit();
            else
                _soundController.Whoosh();

            float spriteOffset = 1.2f;
            transform.Translate(spriteOffset, 0, 0, Space.Self);

            int i = 0;
            float duration = 0.2f;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out IDamagable target))
                {
                    bool result = hit.collider.gameObject.TryGetComponent(out Rigidbody2D rb);
                    if (result)
                    {
                        Sequence sequence = DOTween.Sequence();

                        sequence.AppendCallback(() => DamageCalculation(target))
                            .Append(rb.DOMove(transform.position + transform.right * CharacterAttributes.PencilImpulseValue, duration))
                            .SetLink(rb.gameObject, LinkBehaviour.KillOnDisable);
                    }
                }

                i++;
                if (i >= _targetsNumber)
                    return;
            }
        }

        /// <summary>
        /// Возврат на исходную позицию.
        /// </summary>
        private void Return()
        {
            transform.position = _transformStartPosition.position;
            transform.rotation = _transformStartPosition.rotation;
        }

        private void DamageCalculation(IDamagable target)
        {
            int damage = UnityEngine.Random.Range(CharacterAttributes.PencilMinDamage, CharacterAttributes.PencilMaxDamage);
            target.TakeDamage(damage);
        }

        private void IncreaseTargetsNumber(int value)
        {
            Debug.Log($"Current targets = {_targetsNumber}. Add targets count = {value}");
            _targetsNumber += value;
            Debug.Log($"Увеличено количество целей у карандаша. Текущее значение = {_targetsNumber}");
        }
    }
}
