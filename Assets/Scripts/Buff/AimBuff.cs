using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Attack;
using UnityEngine;

namespace GameBoxProject
{
    class AimBuff : Buff
    {
        private static PencilController pencilController;
        private static PenController penController;

        private CharacterWeaponType _currentWeapon => CharacterAttributes.CurrentWeapon;
        public override string GetDescription()
        {
            if (penController == null)
                penController = FindObjectOfType<PenController>();

            if (pencilController == null)
                pencilController = FindObjectOfType<PencilController>();

            Debug.Log($"Current Weapon: {_currentWeapon}");
            string text = $"{Data.Description}" +
                $"\n\n" +
                $"\nТекущее количество целей: {CurrentTargetsCount()}" +
                $"\nНовое количество целей: {CurrentTargetsCount() + GetNewValue(_currentWeapon)}";
            return text;
        }

        private int CurrentTargetsCount()
        {
            switch (_currentWeapon)
            {
                case CharacterWeaponType.Pen:
                    return penController.TargetNumber;
                case CharacterWeaponType.Pencil:
                    return pencilController.TargetNumber;
            }
            return 0;
        }

        protected override void ConcreteBuffActivate()
        {
            switch (_currentWeapon)
            {
                case CharacterWeaponType.Pen:
                    int count = (int)(penController.TargetNumber * Data.BuffDataByLevels[Level].BuffGain / 100f);
                    Observer.IncreasePenTargetsNumberEvent(count);
                    break;
                case CharacterWeaponType.Pencil:
                    count = (int)(pencilController.TargetNumber * Data.BuffDataByLevels[Level].BuffGain / 100f);
                    Observer.IncreasePencilTargetsNumberEvent(count);
                    break;
            }
        }

        private int GetNewValue(CharacterWeaponType type)
        {
            int result = 0;

            switch(type)
            {
                case CharacterWeaponType.Pen:
                    float count = penController.TargetNumber * Data.BuffDataByLevels[Level+1].BuffGain / 100f;
                    result = Mathf.RoundToInt(count);
                    break;
                case CharacterWeaponType.Pencil:
                    var count2 = pencilController.TargetNumber * Data.BuffDataByLevels[Level+1].BuffGain / 100f;
                    result = Mathf.RoundToInt(count2);
                    Debug.Log($"float = {count2}; int = {result}");
                    break;
            }
            return result;
        }
    }
}