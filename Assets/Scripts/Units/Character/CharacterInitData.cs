using UnityEngine;

namespace Assets.Scripts.Units.Character
{
    [CreateAssetMenu(fileName = "CharacterInitData", menuName = "Data/Character")]
    public class CharacterInitData : ScriptableObject
    {
        [Header("Character")]
        [SerializeField] private float _speed;                      //скорость персонажа
        [SerializeField] private float _armor;                      //броня персонажа

        [Header("Pencil")]
        [SerializeField] private int _pencilHitPoint;               //значение Hp, которое дает карандаш
        [SerializeField] private int _pencilMinDamage;              //минимальный урон карандашом
        [SerializeField] private int _pencilMaxDamage;              //максимальный урон карандашом
        [SerializeField] private float _pencilRechargeTime;         //время перезарядки атаки карандаша
        [SerializeField] private float _pencilAttackDistance;       //дистанция атаки карандаша
        [SerializeField] private float _pencilImpulseValue;         //сила импульса удара карандашом, с которой противника
                                                                    //отбрасывает назад при ударе
        [SerializeField] private int _pencilTargetsNumber;          //количество целей, которым может быть нанесен
                                                                    //урон за 1 удар карандашом

        [Header("Pen")]
        [SerializeField] private int _penHitPoint;                  //значение Hp, которое дает ручка
        [SerializeField] private int _penMinDamage;                 //минимальный урон ручкой
        [SerializeField] private int _penMaxDamage;                 //максимальный урон ручкой
        [SerializeField] private float _penRechargeTime;            //время перезарядки атаки ручки
        [SerializeField] private float _penAttackDistance;          //дистанция выстрела ручки
        [SerializeField] private float _penAttackRadius;            //радиус площади атаки ручки
        [SerializeField] private int _penTargetsNumber;             //начальное количество целей, которым может быть
                                                                    //нанесен урон за 1 выстрел ручкой

        [Header("All weapon")]
        [SerializeField] private float _weaponUpdateMultiplier;     //множитель увеличения урона при апдейте оружия
        [SerializeField] private float _weaponUpdateTime;           //время между апдейтами   


        public float Speed => _speed;
        public float Armor => _armor;

        public int PencilHitPoint => _pencilHitPoint;
        public int PencilMinDamage => _pencilMinDamage;
        public int PencilMaxDamage => _pencilMaxDamage;
        public float PencilRechargeTime => _pencilRechargeTime;
        public float PencilAttackDistance => _pencilAttackDistance;
        public float PencilImpulseValue => _pencilImpulseValue;
        public int PencilTargetsNumber => _pencilTargetsNumber;

        
        public int PenHitPoint => _penHitPoint;
        public int PenMinDamage => _penMinDamage;
        public int PenMaxDamage => _penMaxDamage;
        public float PenRechargeTime => _penRechargeTime;
        public float PenAttackDistance => _penAttackDistance;
        public float PenAttackRadius => _penAttackRadius;
        public int PenTargetsNumber => _penTargetsNumber;
        

        public float WeaponUpdateMultiplier => _weaponUpdateMultiplier;
        public float WeaponUpdateTime => _weaponUpdateTime;
    }
}
