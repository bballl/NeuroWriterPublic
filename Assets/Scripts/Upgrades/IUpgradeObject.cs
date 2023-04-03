namespace Assets.Scripts.Upgrades
{
    internal interface IUpgradeObject
    {
        int GetCurrentLevel(UpgradeType upgradeType);
        void ApplyUpgrade(UpgradeType upgradeType, int price);
        bool CheckLevel(UpgradeType upgradeType);
    }
}
