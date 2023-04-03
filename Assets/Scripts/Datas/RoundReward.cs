using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    [CreateAssetMenu(menuName = "Data/RoundReward")]
    public class RoundReward : ScriptableObject
    {
        [Range(0,100)]
        public int MinValueToCompleteRound = 70;

        public List<GoldByProgress> GoldByProgresses;
    }

    [System.Serializable]
    public class GoldByProgress
    {
        public int MinPercent;
        public int MaxPercent;
        public int GoldCount;
    }
}
