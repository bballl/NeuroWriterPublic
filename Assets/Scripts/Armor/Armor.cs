using System;

namespace GameBoxProject
{
    public class Armor
    {
        //public event Action<float> OnArmorChanged;
        private float _currentArmor;

        public float CurrentArmor => _currentArmor;

        public Armor(float startArmor)
        {
            _currentArmor = startArmor;
        }

        public void SetArmor(float armor)
        {
            if (armor >= 0)
                _currentArmor = armor;
        }
    }
}
