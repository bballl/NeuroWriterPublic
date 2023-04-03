using Assets.Scripts.Units.Character;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    /// <summary>
    /// Временный класс. Передает данные о коэффециенте апгрейда из SO WeaponUpgradeCoeffecientController
    /// в класс CharacterAttributes.
    /// </summary>
    internal class WeaponUpgradeCoeffecientController : MonoBehaviour
    {
        [SerializeField] WeaponUpgradeCoeffecient _coeffecient;

        private void Awake()
        {
            CharacterAttributes.GetWeaponUpgradeCoeffecient(_coeffecient.WeaponUpgradeCoefficient);
        }
    }
}
