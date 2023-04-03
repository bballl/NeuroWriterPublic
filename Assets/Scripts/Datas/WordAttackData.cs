using UnityEngine;

namespace GameBoxProject
{
    [CreateAssetMenu(menuName = "Data/WordAttackData")]
    public class WordAttackData : ScriptableObject
    {
        public float AttackDistance;
        public float TimeBetweenAttack;
        public int CountOfProjectiles;
    }
}
