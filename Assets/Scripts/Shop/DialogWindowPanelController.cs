using Assets.Scripts.Units.Character;
using Assets.Scripts.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class DialogWindowPanelController : MonoBehaviour
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _deleteButton; //временная
        [SerializeField] private TMP_Text _dialogText;

        [SerializeField] private AudioClip _clickSound;
        private AudioSource _audioSource;

        private IUpgradeObject _upgradeObject;
        private UpgradeType _upgradeType;
        private int _price;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = _clickSound;

            _cancelButton.onClick.AddListener(() => Cancel());
            _deleteButton.onClick.AddListener(() => DeleteAll());
            
            Observer.ConfirmPurchaseEvent += Confirm;
            Observer.PurchaseRejectionEvent += PurchaseRejection;
        }

        private void OnDestroy()
        {
            Observer.ConfirmPurchaseEvent -= Confirm;
            Observer.PurchaseRejectionEvent -= PurchaseRejection;
        }

        private void PurchaseRejection(string text)
        {
            Debug.Log("PurchaseRejection");
            _dialogText.text= text;
            _okButton.onClick.AddListener(() => Cancel());
        }

        private void Confirm(IUpgradeObject upgradeObject, UpgradeType upgradeType, int price)
        {
            Debug.Log("Confirm");
            _upgradeObject = upgradeObject;
            _upgradeType = upgradeType;
            _price = price;
            _dialogText.text = ShopDialogWindowText.СonfirmText;
            _okButton.onClick.AddListener(Apply);
        }

        private void Apply()
        {
            Debug.Log("Apply");
            _audioSource.Play();
            _upgradeObject.ApplyUpgrade(_upgradeType, _price);
            
            Observer.UpdateShopDataEvent.Invoke();

            _okButton.onClick.RemoveAllListeners();
            Invoke("DeactivatedGameObject", 0.2f);
        }

        private void Cancel()
        {
            Debug.Log("Cancel");
            _audioSource.Play();
            _okButton.onClick.RemoveAllListeners();
            Invoke("DeactivatedGameObject", 0.2f);
        }

        private void DeactivatedGameObject()
        {
            Debug.Log("DeactivatedGameObject");
            gameObject.SetActive(false);
        }

        private void DeleteAll()
        {
            PlayerPrefs.DeleteAll(); //убрать

            Debug.Log("Все сохраненные данные удалены");
        }
    }
}
