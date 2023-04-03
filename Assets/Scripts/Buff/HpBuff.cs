using UnityEngine;

namespace GameBoxProject
{
    class HpBuff : Buff
    {
        [SerializeField] private MainCharacterController _character;

        private Health _characterHealth => _character.Health;

        public override string GetDescription()
        {
            string text = $"{Data.Description}" +
                $"\n\n" +
                $"\nТекущее максимальное HP: {(int)_characterHealth.MaxHP}" +
                $"\nНовое максимальное HP: {GetNewValue()}";
            return text;
        }

        private int GetNewValue()
        {
            float newValue = _characterHealth.MaxHP * (1 + Data.BuffDataByLevels[Level+1].BuffGain / 100f);
            return (int)newValue;
        }


        protected override void ConcreteBuffActivate()
        {
            float newValue = _characterHealth.MaxHP * (1 + Data.BuffDataByLevels[Level].BuffGain / 100f);
            _characterHealth.UpdateMaxHp(newValue);
        }
    }
}