using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Units.Character.Ability
{
    /// <summary>
    /// Висит на MainCharacter.
    /// </summary>
    internal class AbilityController : MonoBehaviour
    {
        [SerializeField] AbilityData _abilityData;
        [SerializeField] Transform _dronePosition;

        private GameObject _drone;

        private void Awake()
        {
            _drone = Resources.Load<GameObject>("Drone");
            Observer.ActivationAbilityEvent += AbilityActivation;
        }

        private void OnDestroy()
        {
            Observer.ActivationAbilityEvent -= AbilityActivation;
        }

        private void AbilityActivation(AbilityType type)
        {
            float duration = 0;
            switch (type)
            {
                case AbilityType.ExtraDamageAbility:
                    CharacterAttributes.ExtraDamage(true, _abilityData.ExtraDamageMultiplier);
                    duration = _abilityData.ExtraDamageAbilityDuration;

                    Debug.Log("Двойной урон активирован");
                    break;

                case AbilityType.DroneAbility:
                    _drone = GameObject.Instantiate(_drone, _dronePosition);
                    duration = _abilityData.DroneAbilityDuration;
                    break;

                case AbilityType.InvulnerAbility:
                    CharacterAttributes.InvulnerAbility(true);
                    duration = _abilityData.InvulnerAbilityDuration;
                    Debug.Log("Неуязвимость активирована");
                    break;

                default: return;
            }
            
            StartCoroutine(Timer(type, duration));
        }

        private void AbilityDeactivation(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.ExtraDamageAbility:
                    CharacterAttributes.ExtraDamage(false, _abilityData.ExtraDamageMultiplier);
                    Observer.DeactivationAbilityEvent?.Invoke(AbilityType.ExtraDamageAbility);
                    //Debug.Log("Двойной урон закончил действие");
                    break;

                case AbilityType.DroneAbility:
                    Destroy(_drone);
                    Observer.DeactivationAbilityEvent?.Invoke(AbilityType.DroneAbility);
                    _drone = Resources.Load<GameObject>("Drone");
                    break;

                case AbilityType.InvulnerAbility:
                    CharacterAttributes.InvulnerAbility(false);
                    Observer.DeactivationAbilityEvent?.Invoke(AbilityType.InvulnerAbility);
                    //Debug.Log("Действие неуязвимости окончено");
                    break;
            }
        }

        private IEnumerator Timer(AbilityType type, float duration)
        {
            yield return new WaitForSeconds(duration);
            AbilityDeactivation(type);
        }
    }
}
