using Assets.Scripts.SaveLoadData;
using Assets.Scripts.Upgrades;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class PencilDamageUpgradePanelController : ShopUpgradePanelController
    {
        private void Start()
        {
            _upgradeObject = new PencilUpgrade();
            _upgradeType = UpgradeType.DamageUpgrade;
            UpdateData();
        }
    }
}
