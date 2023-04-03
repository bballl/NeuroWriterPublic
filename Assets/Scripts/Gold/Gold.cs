using System;
using UnityEngine;

namespace GameBoxProject
{
    public class Gold 
    {
        public event Action<int> OnGoldCountChanged;

        private int _current;

        public int Current
        {
            get => _current;
            private set
            {
                _current = Mathf.Clamp(value, 0, int.MaxValue);
                OnGoldCountChanged?.Invoke(_current);
            }
        }

        public Gold(int startGold = 0)
        {
            Current = startGold;
        }

        public void AddGold(int value)
        {
            Current += value;
        }
    }
}