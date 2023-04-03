using Assets.Scripts.Upgrades;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class PencilAttackSpeedUpgradeController : ShopUpgradePanelController
    {
        private void Start()
        {
            _upgradeObject = new PencilUpgrade();
            _upgradeType = UpgradeType.AttackSpeedUpgrade;
            UpdateData();
        }
    }
}
