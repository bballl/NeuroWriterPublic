using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "ShopPrice", menuName = "Data/Shop")]
    public class ShopPrice : ScriptableObject
    {
        [SerializeField] private int _levelOneCost;
        [SerializeField] private int _levelTwoCost;
        [SerializeField] private int _levelThreeCost;

        public int LevelOneCost => _levelOneCost;
        public int LevelTwoCost => _levelTwoCost;
        public int LevelThreeCost => _levelThreeCost;
    }
}
