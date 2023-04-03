using UnityEngine;

namespace Assets.Scripts.Units.Character.Ability
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Data/Ability")]
    internal class AbilityData : ScriptableObject
    {
        [Header("Extra Damage Ability")]
        [SerializeField] private float _extraDamageAbilityDuration; //длительность экстра-урона
        [SerializeField] private int _extraDamageMultiplier;        //множитель для экстра-урона (на эту величину умножаем базовый урон)

        [Header("Invulner Ability")]
        [SerializeField] private float _invulnerAbilityDuration;    //длительность действия неуязвимости

        [Header("Drone Ability")]
        [SerializeField] private float _droneAbilityDuration;       //длительность действия дрона
        [SerializeField] private float _droneAttackRadius;          //радиус атаки дрона
        [SerializeField] private float _droneRechargeTime;          //время перезарядки между выстрелами дрона
        [SerializeField] private float _droneDamage;                //урон дрона

        public float ExtraDamageAbilityDuration => _extraDamageAbilityDuration;
        public int ExtraDamageMultiplier => _extraDamageMultiplier;

        public float InvulnerAbilityDuration => _invulnerAbilityDuration;
        
        public float DroneAbilityDuration => _droneAbilityDuration;
        public float DroneAttackRadius => _droneAttackRadius;
        public float DroneRechargeTime => _droneRechargeTime;
        public float DroneDamage => _droneDamage;
    }
}
