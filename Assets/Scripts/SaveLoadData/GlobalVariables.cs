namespace Assets.Scripts.SaveLoadData
{
    internal struct GlobalVariables
    {
        public static readonly string Level = "Level"; //Уровень игры (для локаций и оружия)

        public static readonly string Coins = "Coins"; //монеты (золото) персонажа
        
        public static readonly string PencilHpUpgradeLevel = "PencilHpUpgradeLevel"; //уровень апгрейда HP карандаша
        public static readonly string PencilAttackSpeedUpgradeLevel = "PencilAttackSpeedUpgradeLevel"; //уровень апгрейда скорости атаки карандаша
        public static readonly string PencilDamageUpgradeLevel = "PencilDamageUpgradeLevel"; //уровень апгрейда урона карандаша

        public static readonly string PenHpUpgradeLevel = "PenHpUpgradeLevel"; //уровень апгрейда HP ручки
        public static readonly string PenAttackSpeedUpgradeLevel = "PenAttackSpeedUpgradeLevel"; //уровень апгрейда скорости атаки ручки
        public static readonly string PenDamageUpgradeLevel = "PenDamageUpgradeLevel"; //уровень апгрейда урона ручки

        //коэффециенты уровня апгрейда карандаша и ручки
        public static readonly string PencilHpLevelCoefficient = "PencilHpLevelCoefficient";
        public static readonly string PencilAttackSpeedLevelCoefficient = "PencilAttackSpeedLevelCoefficient";
        public static readonly string PencilDamageLevelCoefficient = "PencilDamageLevelCoefficient";

        public static readonly string PenHpLevelCoefficient = "PenHpLevelCoefficient";
        public static readonly string PenAttackSpeedLevelCoefficient = "PenAttackSpeedLevelCoefficient";
        public static readonly string PenDamageLevelCoefficient = "PenDamageLevelCoefficient";

        //музыка и звуки
        public static readonly string MasterVolume = "MasterVolume";
        public static readonly string SoundsVolume = "SoundsVolume";
        public static readonly string MusicVolume = "MusicVolume";
    }
}
