using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Attack;
using UnityEngine;

namespace GameBoxProject
{
    class AttackBuff : Buff
    {
        public override string GetDescription()
        {
            float currentTime = 0f;

            switch (CharacterAttributes.CurrentWeapon)
            {
                case CharacterWeaponType.Pen:
                    currentTime = CharacterAttributes.PenRechargeTime;
                    break;
                case CharacterWeaponType.Pencil:
                    currentTime = CharacterAttributes.PencilRechargeTime;
                    break;
            }

            string text = $"{Data.Description}" +
                $"\n\n" +
                $"\nТекущее время перезарядки: {currentTime.ToString("F2")}" +
                $"\nНовое время перезарядки: {GetNewValue(currentTime).ToString("F2")}";
            return text;
        }

        private float GetNewValue(float startValue)
        {
            float newValue = startValue * Data.BuffDataByLevels[Level+1].BuffGain / 100f;

            return newValue;
        }

        protected override void ConcreteBuffActivate()
        {
            float multiplier = Data.BuffDataByLevels[Level].BuffGain / 100f;
            CharacterAttributes.ChangeRechargeTime(multiplier);
        }
    }
}