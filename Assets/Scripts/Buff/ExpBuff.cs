using UnityEngine;

namespace GameBoxProject
{
    class ExpBuff : Buff
    {
        [SerializeField] private ExperienceHolder _experienceHolder;

        public override string GetDescription()
        {
            string text = $"{Data.Description}" +
                $"\n\n" +
                $"\nТекущий коэффициент опыта: {_experienceHolder.Multiplier * 100}%" +
                $"\nНовый коэффициент опыта: {GetNewValue()}%";
            return text;
        }

        private string GetNewValue()
        {
            float newValue = _experienceHolder.Multiplier * (1 + Data.BuffDataByLevels[Level+1].BuffGain / 100f);
            return ((int)(newValue * 100f)).ToString();
        }

        protected override void ConcreteBuffActivate()
        {
            float multiply = 1 + Data.BuffDataByLevels[Level].BuffGain / 100f;
            _experienceHolder.SetMultiplier(multiply);
        }
    }
}
