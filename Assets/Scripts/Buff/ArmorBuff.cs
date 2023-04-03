using UnityEngine;

namespace GameBoxProject
{
    class ArmorBuff : Buff
    {
        [SerializeField] private MainCharacterController _character;

        private Armor _characterArmor => _character.Armor;

        public override string GetDescription()
        {
            string text = $"{Data.Description}" +
                $"\n\n" +
                $"\nТекущая броня: {_characterArmor.CurrentArmor.ToString("F1")}" +
                $"\nНовая броня: {GetNewValue().ToString("F1")}";
            return text;
        }

        private float GetNewValue()
        {
            float newValue = _characterArmor.CurrentArmor * (1 + Data.BuffDataByLevels[Level+1].BuffGain / 100f);
            return newValue;
        }

        protected override void ConcreteBuffActivate()
        {
            float newValue = _characterArmor.CurrentArmor * (1 + Data.BuffDataByLevels[Level].BuffGain / 100f);
            _characterArmor.SetArmor(newValue);

            Debug.Log($"Armor = {_characterArmor.CurrentArmor}");
        }
    }
}
