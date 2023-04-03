using System;
using UnityEngine;

namespace GameBoxProject
{
    public class Experience
    {
        public event Action<int> OnExperienceCountChanged;

        public int Current
        {
            get => _current;
            private set
            {
                _current = Mathf.Clamp(value, 0, int.MaxValue);
                OnExperienceCountChanged?.Invoke(_current);
            }
        }

        private int _current;

        public Experience(int startExp = 0)
        {
            Current = startExp;
        }

        public void AddExperience(int value)
        {
            Current += value;
        }
    }
}