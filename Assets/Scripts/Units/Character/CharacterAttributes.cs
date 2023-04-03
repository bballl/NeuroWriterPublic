using Assets.Scripts.Units.Character.Attack;
using UnityEngine;
using System;
using GameBoxProject;
using Assets.Scripts.Upgrades;
using Assets.Scripts.SaveLoadData;

namespace Assets.Scripts.Units.Character
{
    /// <summary>
    /// Текущие параметры персонажа.
    /// </summary>
    internal static class CharacterAttributes
    {
        private static float _health;
        private static float _speed;
        private static float _armor;
        private static int _coins;
        private static CharacterWeaponType _currentWeapon;

        private static int _pencilMinDamage;
        private static int _pencilMaxDamage;
        private static float _pencilRechargeTime;
        private static float _pencilAttackDistance;
        private static float _pencilImpulseValue;
        private static int _pencilTargetsNumber;
        
        private static float _pencilHpLevelCoefficient = 1;
        private static float _pencilDamageLevelCoefficient = 1;
        private static float _pencilAttackSpeedLevelCoefficient = 1;

        private static int _penMinDamage;
        private static int _penMaxDamage;
        private static float _penRechargeTime;
        private static float _penAttackDistance;
        private static int _penTargetsNumber;
        private static float _penAttackRadius;

        
        private static float _penHpLevelCoefficient = 1;
        private static float _penDamageLevelCoefficient = 1;
        private static float _penAttackSpeedLevelCoefficient = 1;

        private static float _weaponUpdateMultiplier;
        private static float _weaponUpdateTime;
        private static float _weaponUpgradeCoefficient;

        private static bool _isInvulnerAbilityActivated;

        public static float Health => _health;
        public static float Speed => _speed;
        public static float Armor => _armor;
        public static int Coins => _coins;
        public static CharacterWeaponType CurrentWeapon => _currentWeapon;


        public static float PencilRechargeTime => _pencilRechargeTime;
        public static int PencilMinDamage => _pencilMinDamage;
        public static int PencilMaxDamage => _pencilMaxDamage;
        public static float PencilAttackDistance => _pencilAttackDistance;
        public static float PencilImpulseValue => _pencilImpulseValue;
        public static int PencilTargetsNumber => _pencilTargetsNumber;

        public static float PenRechargeTime => _penRechargeTime;
        public static int PenMinDamage => _penMinDamage;
        public static int PenMaxDamage => _penMaxDamage;
        public static float PenAttackDistance => _penAttackDistance;
        public static int PenTargetsNumber => _penTargetsNumber;
        public static float PenAttackRadius => _penAttackRadius;

        public static float WeaponUpdateTime => _weaponUpdateTime;

        public static bool IsInvulnerAbilityActivated => _isInvulnerAbilityActivated;

        static CharacterAttributes()
        {
            LoadData();
            Observer.SaveCoinsEvent += SaveCoins;

            Debug.Log($"Начальное значение монет: {_coins}");
        }

        private static void LoadData()
        {
            var loadData = new LoadData();
            
            _coins = loadData.GetIntData(GlobalVariables.Coins);
            //if (_coins == 0)
            //    _coins = 250;

            _pencilHpLevelCoefficient = loadData.GetFloatData(GlobalVariables.PencilHpLevelCoefficient, 1);
            _pencilAttackSpeedLevelCoefficient = loadData.GetFloatData(GlobalVariables.PencilAttackSpeedLevelCoefficient, 1);
            _pencilDamageLevelCoefficient = loadData.GetFloatData(GlobalVariables.PencilDamageLevelCoefficient, 1);

            _penHpLevelCoefficient = loadData.GetFloatData(GlobalVariables.PenHpLevelCoefficient, 1);
            _penAttackSpeedLevelCoefficient = loadData.GetFloatData(GlobalVariables.PenAttackSpeedLevelCoefficient, 1);
            _penDamageLevelCoefficient = loadData.GetFloatData(GlobalVariables.PenDamageLevelCoefficient, 1);
        }

        internal static void GameLevelDataInit(CharacterInitData data, bool needSubscribe = true)
        {
            _speed = data.Speed;
            _armor = data.Armor;

            _pencilMinDamage = (int)(data.PencilMinDamage * _pencilDamageLevelCoefficient);
            _pencilMaxDamage = (int)(data.PencilMaxDamage * _pencilDamageLevelCoefficient);
            _pencilRechargeTime = data.PencilRechargeTime * _pencilAttackSpeedLevelCoefficient;
            _pencilAttackDistance = data.PencilAttackDistance;
            _pencilImpulseValue = data.PencilImpulseValue;
            _pencilTargetsNumber = data.PencilTargetsNumber;

            _penMinDamage = (int)(data.PenMinDamage * _penDamageLevelCoefficient);
            _penMaxDamage = (int)(data.PenMaxDamage * _penDamageLevelCoefficient);
            _penRechargeTime = data.PenRechargeTime * _penAttackSpeedLevelCoefficient;
            _penAttackDistance = data.PenAttackDistance;
            _penAttackRadius= data.PenAttackRadius;
            _penTargetsNumber= data.PenTargetsNumber;

            _weaponUpdateMultiplier = data.WeaponUpdateMultiplier;
            _weaponUpdateTime= data.WeaponUpdateTime;

            if (_currentWeapon == CharacterWeaponType.Pen)
            {
                _health = data.PenHitPoint * _penHpLevelCoefficient;
            }
            else
            {
                _health = data.PencilHitPoint * _pencilHpLevelCoefficient;
            }
        }

        public static void GetWeaponUpgradeCoeffecient(float value)
        {
            _weaponUpgradeCoefficient = value;
            Debug.Log($"weaponUpgradeCoefficient = {_weaponUpgradeCoefficient}");
        }

        public static void GetWeapon(CharacterWeaponType type)
        {
            if (type == CharacterWeaponType.Pencil)
            {
                _currentWeapon = CharacterWeaponType.Pencil;
            }

            if (type == CharacterWeaponType.Pen)
            {
                _currentWeapon = CharacterWeaponType.Pen;
            }
        }
        
        internal static void ExtraDamage(bool isActive, int multiplier)
        {
            if (isActive)
            {
                _penMinDamage = _penMinDamage * multiplier;
                _penMaxDamage = _penMaxDamage * multiplier;
                _pencilMinDamage = _pencilMinDamage * multiplier;
                _pencilMaxDamage = _pencilMaxDamage * multiplier;
            }
            else
            {
                _penMinDamage = _penMinDamage / multiplier;
                _penMaxDamage = _penMaxDamage / multiplier;
                _pencilMinDamage = _pencilMinDamage / multiplier;
                _pencilMaxDamage = _pencilMaxDamage / multiplier;
            }
        }

        internal static void InvulnerAbility(bool isActive)
        {
            if (isActive)
                _isInvulnerAbilityActivated = true;
            else
                _isInvulnerAbilityActivated = false;
        }

        public static void ChangeCharacterSpeed(float multiplier)
        {
            _speed *= multiplier;
        }

        public static void ChangeRechargeTime(float multiplier)
        {
            _pencilRechargeTime *= multiplier;
            _penRechargeTime *= multiplier;
        }

        public static void WeaponUpdate() //увеличение значений по уровню
        {
            Debug.Log("Сработал CharacterAttributes.WeaponUpdate()");
            
            _penMinDamage = (int)(_penMinDamage * _weaponUpdateMultiplier);
            _penMaxDamage = (int)(_penMaxDamage * _weaponUpdateMultiplier);
            _pencilMinDamage = (int)(_pencilMinDamage * _weaponUpdateMultiplier);
            _pencilMaxDamage = (int)(_pencilMaxDamage * _weaponUpdateMultiplier);

            Debug.Log("Произошел апдейт оружия");
            if (_currentWeapon == CharacterWeaponType.Pencil)
            {
                Debug.Log($"Текущий минимальный урон: {_pencilMinDamage}");
                Debug.Log($"Текущий максимальный урон: {_pencilMaxDamage}");
            }
            else
            {
                Debug.Log($"Текущий минимальный урон: {_penMinDamage}");
                Debug.Log($"Текущий максимальный урон: {_penMaxDamage}");
            }
        }

        public static void WeaponUpgrade(IUpgradeObject upgradeObject, UpgradeType type)
        {
            if (upgradeObject.GetType() == typeof(PencilUpgrade)) 
            {
                PencilUpgrade(type);
            }

            if (upgradeObject.GetType() == typeof(PenUpgrade))
            {
                PenUpgrade(type);
            }
        }
        
        private static void PencilUpgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.HpUpgrade:
                    Debug.Log("Запущен CharacterAttributes.PencilUpgrade - HpUpgrade");
                    _pencilHpLevelCoefficient += _weaponUpgradeCoefficient;
                    new SaveData(GlobalVariables.PencilHpLevelCoefficient, _pencilHpLevelCoefficient); //сохранение
                    
                    Debug.Log($"_pencilHpLevelCoefficient равен {_pencilHpLevelCoefficient}");
                    break;
                case UpgradeType.AttackSpeedUpgrade:
                    Debug.Log("Запущен CharacterAttributes.PencilUpgrade - AttackSpeedUpgrade");
                    _pencilAttackSpeedLevelCoefficient -= _weaponUpgradeCoefficient; //здесь вычитаем, т.к. скорость перезарядки
                    new SaveData(GlobalVariables.PencilAttackSpeedLevelCoefficient, _pencilAttackSpeedLevelCoefficient);
                    
                    Debug.Log($"_pencilAttackSpeedLevelCoefficient равен {_pencilAttackSpeedLevelCoefficient}");
                    break;
                case UpgradeType.DamageUpgrade:
                    Debug.Log("Запущен CharacterAttributes.PencilUpgrade - DamageUpgrade");
                    _pencilDamageLevelCoefficient += _weaponUpgradeCoefficient;
                    new SaveData(GlobalVariables.PencilDamageLevelCoefficient, _pencilDamageLevelCoefficient);
                    
                    Debug.Log($"_pencilDamageLevelCoefficient равен {_pencilDamageLevelCoefficient}");
                    break;
            }
        }

        private static void PenUpgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.HpUpgrade:
                    Debug.Log("Запущен CharacterAttributes.PenUpgrade - HpUpgrade");
                    _penHpLevelCoefficient += _weaponUpgradeCoefficient;
                    new SaveData(GlobalVariables.PenHpLevelCoefficient, _penHpLevelCoefficient);
                    
                    Debug.Log($"_penHpLevelCoefficient равен {_penHpLevelCoefficient}");
                    break;
                case UpgradeType.AttackSpeedUpgrade:
                    Debug.Log("Запущен CharacterAttributes.PenUpgrade - AttackSpeedUpgrade");
                    _penAttackSpeedLevelCoefficient -= _weaponUpgradeCoefficient;         //здесь вычитаем, т.к. скорость перезарядки
                    new SaveData(GlobalVariables.PenAttackSpeedLevelCoefficient, _penAttackSpeedLevelCoefficient);
                    
                    Debug.Log($"_penAttackSpeedLevelCoefficient равен {_penAttackSpeedLevelCoefficient}");
                    break;
                case UpgradeType.DamageUpgrade:
                    Debug.Log("Запущен CharacterAttributes.PenUpgrade - DamageUpgrade");
                    _penDamageLevelCoefficient += _weaponUpgradeCoefficient;
                    new SaveData(GlobalVariables.PenDamageLevelCoefficient, _penDamageLevelCoefficient);
                    
                    Debug.Log($"_penDamageLevelCoefficient равен {_penDamageLevelCoefficient}");
                    break;
            }
        }

        public static void Purchase(int price)
        {
            _coins -= price;
            Debug.Log($"Остаток coins: {_coins}");

            new SaveData(GlobalVariables.Coins, _coins);
            Debug.Log($"CharacterAttributes.Purchase() выполнено сохранение _coins");
        }

        private static void SaveCoins(int value)
        {
            _coins += value;
            new SaveData(GlobalVariables.Coins, _coins);
            Debug.Log($"Добавлено монет: {value}");
            Debug.Log($"Общее количество монет: {_coins}");
        }
    }
}
