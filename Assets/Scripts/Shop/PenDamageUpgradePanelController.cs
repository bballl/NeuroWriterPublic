using Assets.Scripts.Upgrades;

namespace Assets.Scripts.Shop
{
    public class PenDamageUpgradePanelController : ShopUpgradePanelController
    {
        private void Start()
        {
            _upgradeObject = new PenUpgrade();
            _upgradeType = UpgradeType.DamageUpgrade;
            UpdateData();
        }
    }
}
