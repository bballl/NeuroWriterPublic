using Assets.Scripts.Shop;
using Assets.Scripts.Units.Character.Ability;
using Assets.Scripts.Units.Character.Attack;
using Assets.Scripts.Upgrades;
using System;

namespace Assets.Scripts.Units.Character
{
    internal struct Observer
    {
        /// <summary>
        /// Событие смены оружия персонажа.
        /// </summary>
        public static Action<CharacterWeaponType> ChangeWeaponEvent;

        /// <summary>
        /// Событие активации абилки.
        /// </summary>
        public static Action<AbilityType> ActivationAbilityEvent;

        /// <summary>
        /// Событие деактивации абилки.
        /// </summary>
        public static Action<AbilityType> DeactivationAbilityEvent;

        /// <summary>
        /// Событие открытия диалогового окна в магазине подтверждения покупки.
        /// </summary>
        public static Action<IUpgradeObject, UpgradeType, int> ConfirmPurchaseEvent;

        /// <summary>
        /// Событие открытия диалогового окна в магазине с отказом в покупке.
        /// </summary>
        public static Action<string> PurchaseRejectionEvent;

        /// <summary>
        /// Событие активации/деактивации диалогового окна магазина.
        /// </summary>
        public static Action<bool> ShopDialogWindowActivationEvent;

        /// <summary>
        /// Обновление данных магазина.
        /// </summary>
        public static Action UpdateShopDataEvent;

        /// <summary>
        /// Событие увеличение количества целей у карандаша.
        /// </summary>
        public static Action<int> IncreasePencilTargetsNumberEvent;

        /// <summary>
        /// Событие увеличение количества целей у ручки.
        /// </summary>
        public static Action<int> IncreasePenTargetsNumberEvent;

        /// <summary>
        /// Событие сохранения монет, полученных игроком.
        /// </summary>
        public static Action<int> SaveCoinsEvent;

        /// <summary>
        /// Событие окончания основного игрового уровня.
        /// </summary>
        public static Action GameLevelFinishedEvent;


        public static Action SaveShopDataEvent; //?

        public static Action EndGameEvent;
    }
}
