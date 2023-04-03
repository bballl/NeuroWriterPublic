using UnityEngine;

namespace Assets.Scripts.Units.Character
{
    [CreateAssetMenu(fileName = "WeaponUpgradeCoeffecient", menuName = "Data/WeaponUpgradeCoeffecient")]
    public class WeaponUpgradeCoeffecient : ScriptableObject
    {
        [SerializeField] private float _weaponUpgradeCoefficient;

        public float WeaponUpgradeCoefficient => _weaponUpgradeCoefficient;
    }
}
