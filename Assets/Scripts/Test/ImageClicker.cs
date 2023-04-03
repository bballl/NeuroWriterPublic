using System;
using UnityEngine;

namespace GameBoxProject
{
    public class ImageClicker : MonoBehaviour
    {
        public static event Action<Letter> OnLetterDead;

        public SpriteRenderer Icon;
        public Letter Data;
        private IDamagable _damagable;

        private void OnMouseUp()
        {
            if (TryGetComponent(out IDamagable _damagable))
            {
                _damagable.TakeDamage(1f);
            }


            //OnLetterDead?.Invoke(Data);
            //Destroy(gameObject);
        }
    }
}