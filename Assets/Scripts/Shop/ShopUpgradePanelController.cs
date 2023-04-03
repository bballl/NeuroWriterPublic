using Assets.Scripts.Units.Character;
using Assets.Scripts.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public abstract class ShopUpgradePanelController : MonoBehaviour
    {
        [SerializeField] protected TMP_Text CurrentPriceText;
        [SerializeField] protected ShopPrice ShopPrice;
        [SerializeField] protected GameObject[] _markers;
        [SerializeField] protected AudioClip _clickSound;

        protected Button BuyButton;

        internal AudioSource _audioSource;
        internal UpgradeType _upgradeType;
        internal WeaponUpgrade _upgradeObject;
        internal int _currentUpgradeLevel;
        internal int _price;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = _clickSound;
            
            BuyButton = GetComponentInChildren<Button>();
            BuyButton.onClick.AddListener(() => PurchaseRequest());
            BuyButton.onClick.AddListener(() => _audioSource.Play());

            Observer.UpdateShopDataEvent += UpdateData;
        }

        private void OnDestroy()
        {
            Observer.UpdateShopDataEvent -= UpdateData;
        }

        /// <summary>
        /// Запрос на покупку.
        /// </summary>
        internal void PurchaseRequest()
        {
            Observer.ShopDialogWindowActivationEvent.Invoke(true);
            
            if (CharacterAttributes.Coins < _price) //проверка достаточности денег
            {
                Observer.PurchaseRejectionEvent.Invoke(ShopDialogWindowText.NotEnoughCoinsText);
            }
            else if (_upgradeObject.CheckLevel(_upgradeType) == false) //проверка на максимальный уровень апгрейда
            {
                Observer.PurchaseRejectionEvent.Invoke(ShopDialogWindowText.MaxLevelText);
            }
            else
            {
                Observer.ConfirmPurchaseEvent.Invoke(_upgradeObject, _upgradeType, _price);
            }
        }

        internal void UpdateData()
        {
            _currentUpgradeLevel = _upgradeObject.GetCurrentLevel(_upgradeType);

            switch (_currentUpgradeLevel)
            {
                case 0:
                    _price = ShopPrice.LevelOneCost;
                    break;
                case 1:
                    _price = ShopPrice.LevelTwoCost;
                    break;
                case 2:
                    _price = ShopPrice.LevelThreeCost;
                    break;
                case 3:
                    _price = 0;
                    break;
                default: Debug.Log("Ошибка. ShopUpgradePanelController.UpdatePrice() не удалось определить значение _price");
                    break;
            }

            UpdateView(_price);
        }

        private void UpdateView(int price)
        {
            CurrentPriceText.text = $"Стоимость: {_price}";
            ShowMarkers(_currentUpgradeLevel);
        }

        private void ShowMarkers(int value)
        {
            if (value > _markers.Length)
            {
                Debug.Log($"ShowMarkers() ошибка данных: value = {value}");
                return;
            }

            for (int i = 0; i < _markers.Length; i++)
            {
                if (i < value)
                {
                    _markers[i].SetActive(true);
                }
                else
                    _markers[i].SetActive(false);
            }
        }
    }
}
