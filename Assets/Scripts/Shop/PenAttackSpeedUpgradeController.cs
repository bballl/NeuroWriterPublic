using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Upgrades;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class PenAttackSpeedUpgradeController : ShopUpgradePanelController
    {
        private void Start()
        {
            _upgradeObject = new PenUpgrade();
            _upgradeType = UpgradeType.AttackSpeedUpgrade;
            UpdateData();
        }
    }
}
