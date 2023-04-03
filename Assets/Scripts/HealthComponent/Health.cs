using System;
using UnityEngine;

namespace GameBoxProject
{
    public class Health : IHealthRecovery
    {
        public event Action<object, float, float> OnHealthChanged;
        public event Action<object> OnPersonDead;

        public bool IsAlive { get; private set; }
        public float MaxHP { get; private set; }
        public float Hp
        {
            get => _currentHp;
            private set
            {
                _currentHp = Mathf.Clamp(value, 0f, MaxHP);

                if (_currentHp <= 0)
                {
                    IsAlive = false;
                    OnHealthChanged?.Invoke(this, Hp, MaxHP);
                    OnPersonDead?.Invoke(this);
                }
                else
                    OnHealthChanged?.Invoke(this, Hp, MaxHP);
            }
        }


        private float _currentHp;


        public Health(float maxHp)
        {
            MaxHP = maxHp;
            Hp = MaxHP;
            IsAlive = true;
        }

        public void SetValues(float maxHp)
        {
            MaxHP = maxHp;
            Hp = MaxHP;
            IsAlive = true;
        }

        public void UpdateMaxHp(float maxHp)
        {
            float currentPersent = Hp / MaxHP;
            MaxHP = Mathf.Abs(maxHp);
            Hp = Mathf.Abs(currentPersent * MaxHP);
            OnHealthChanged?.Invoke(this, Hp, MaxHP);
        }

        public void TakeDamage(float damage) => Hp -= damage;

        public void RecoveryHealth(float value) => Hp += value;
    }
}