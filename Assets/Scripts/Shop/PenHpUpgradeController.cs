using Assets.Scripts.Upgrades;

namespace Assets.Scripts.Shop
{
    public class PenHpUpgradeController : ShopUpgradePanelController
    {
        private void Start()
        {
            _upgradeObject = new PenUpgrade();
            _upgradeType = UpgradeType.HpUpgrade;
            UpdateData();
        }
    }
}
