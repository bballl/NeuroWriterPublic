using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Units.Character;
using UnityEngine;

namespace Assets.Scripts.Upgrades
{
    internal abstract class WeaponUpgrade : IUpgradeObject
    {
        protected int CurrentHpUpgradeLevel;
        protected int CurrentAttackSpeedUpgradeLevel;
        protected int CurrentDamageUpgradeLevel;

        public WeaponUpgrade() //получение данных из PlayerPrefs
        {
            var loadData = new LoadData();
            if (this.GetType() == typeof(PencilUpgrade))
            {
                CurrentHpUpgradeLevel = loadData.GetIntData(GlobalVariables.PencilHpUpgradeLevel);
                CurrentAttackSpeedUpgradeLevel = loadData.GetIntData(GlobalVariables.PencilAttackSpeedUpgradeLevel);
                CurrentDamageUpgradeLevel = loadData.GetIntData(GlobalVariables.PencilDamageUpgradeLevel);
            }

            if (this.GetType() == typeof(PenUpgrade))
            {
                CurrentHpUpgradeLevel = loadData.GetIntData(GlobalVariables.PenHpUpgradeLevel);
                CurrentAttackSpeedUpgradeLevel = loadData.GetIntData(GlobalVariables.PenAttackSpeedUpgradeLevel);
                CurrentDamageUpgradeLevel = loadData.GetIntData(GlobalVariables.PenDamageUpgradeLevel);
            }
        }
        
        public int GetCurrentLevel(UpgradeType upgradeType)
        {
            switch (upgradeType)
            {
                case UpgradeType.HpUpgrade:
                    return CurrentHpUpgradeLevel;

                case UpgradeType.AttackSpeedUpgrade:
                    return CurrentAttackSpeedUpgradeLevel;

                case UpgradeType.DamageUpgrade:
                    return CurrentDamageUpgradeLevel;

                default: return 0;
            }
        }
        
        public bool CheckLevel(UpgradeType upgradeType)
        {
            bool checkLevelResult = false;
            switch (upgradeType)
            {
                case UpgradeType.HpUpgrade:
                    if (CurrentHpUpgradeLevel < (byte)UpgradeState.MaxUpgradeLevel)
                    {
                        checkLevelResult = true;
                        Debug.Log($"Текущий уровень апгрейда: {CurrentHpUpgradeLevel}");
                    }
                    else
                    {
                        Debug.Log("Достигнут максимальный уровень");
                    }
                    break;

                case UpgradeType.AttackSpeedUpgrade:
                    if (CurrentAttackSpeedUpgradeLevel < (byte)UpgradeState.MaxUpgradeLevel)
                    {
                        checkLevelResult = true;
                        Debug.Log($"Текущий уровень апгрейда: {CurrentAttackSpeedUpgradeLevel}");
                    }
                    else
                    {
                        Debug.Log("Достигнут максимальный уровень");
                    }
                    break;

                case UpgradeType.DamageUpgrade:
                    if (CurrentDamageUpgradeLevel < (byte)UpgradeState.MaxUpgradeLevel)
                    {
                        checkLevelResult = true;
                        Debug.Log($"Текущий уровень апгрейда: {CurrentDamageUpgradeLevel}");
                    }
                    else
                    {
                        Debug.Log("Достигнут максимальный уровень");
                    }
                    break;
            }
            
            return checkLevelResult;
        }
        
        public void ApplyUpgrade(UpgradeType upgradeType, int price)
        {
            switch (upgradeType)
            {
                case UpgradeType.HpUpgrade:
                    CurrentHpUpgradeLevel++;
                    CharacterAttributes.WeaponUpgrade(this, upgradeType);

                    Debug.Log($"Куплен HpUpgrade");
                    break;
                
                case UpgradeType.AttackSpeedUpgrade:
                    CurrentAttackSpeedUpgradeLevel++;
                    CharacterAttributes.WeaponUpgrade(this, upgradeType);
                    
                    Debug.Log($"Куплен AttackSpeedUpgrade");
                    break;
                
                case UpgradeType.DamageUpgrade:
                    CurrentDamageUpgradeLevel++;
                    CharacterAttributes.WeaponUpgrade(this, upgradeType);
                    
                    Debug.Log($"Куплен DamageUpgrade");
                    break;
            }

            CharacterAttributes.Purchase(price);
            Save(upgradeType);
        }

        /// <summary>
        /// Сохранение данных об уровне апгрейда.
        /// </summary>
        private void Save(UpgradeType upgradeType)
        {
            if (this.GetType() == typeof(PencilUpgrade))
            {
                switch (upgradeType)
                {
                    case UpgradeType.HpUpgrade:
                        new SaveData(GlobalVariables.PencilHpUpgradeLevel, CurrentHpUpgradeLevel);
                        break;
                    case UpgradeType.AttackSpeedUpgrade:
                        new SaveData(GlobalVariables.PencilAttackSpeedUpgradeLevel, CurrentAttackSpeedUpgradeLevel);
                        break;
                    case UpgradeType.DamageUpgrade:
                        new SaveData(GlobalVariables.PencilDamageUpgradeLevel, CurrentDamageUpgradeLevel);
                        break;
                }
            }

            if (this.GetType() == typeof(PenUpgrade))
            {
                switch (upgradeType)
                {
                    case UpgradeType.HpUpgrade:
                        new SaveData(GlobalVariables.PenHpUpgradeLevel, CurrentHpUpgradeLevel);
                        break;
                    case UpgradeType.AttackSpeedUpgrade:
                        new SaveData(GlobalVariables.PenAttackSpeedUpgradeLevel, CurrentAttackSpeedUpgradeLevel);
                        break;
                    case UpgradeType.DamageUpgrade:
                        new SaveData(GlobalVariables.PenDamageUpgradeLevel, CurrentDamageUpgradeLevel);
                        break;
                }
            }
        }
    }
}
