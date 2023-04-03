using Assets.Scripts.Units.Character;
using UnityEngine;

namespace GameBoxProject
{
    class SpeedBuff : Buff
    {
        public override string GetDescription()
        {
            string text = $"{Data.Description}" +
                $"\n\n" +
                $"\nТекущая скорость персонажа: {CharacterAttributes.Speed.ToString("F1")}" +
                $"\nНовая скорость персонажа: {GetNewValue().ToString("F1")}";
            return text;
        }

        private float GetNewValue()
        {
            float newValue = CharacterAttributes.Speed * (1 + Data.BuffDataByLevels[Level+1].BuffGain / 100f);
            return newValue;
        }

        protected override void ConcreteBuffActivate()
        {
            float multyplier = 1 + Data.BuffDataByLevels[Level].BuffGain / 100f;
            CharacterAttributes.ChangeCharacterSpeed(multyplier);
        }
    }
}
