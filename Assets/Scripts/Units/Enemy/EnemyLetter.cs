using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoxProject
{
    class EnemyLetter : Enemy
    {
        public static event Action<Letter> OnDead;

        protected override void Dead(object obj)
        {
            base.Dead(obj);
            OnDead?.Invoke(_letterData);
        }
    }
}
