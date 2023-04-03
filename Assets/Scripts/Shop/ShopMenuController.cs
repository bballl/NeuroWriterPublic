using Assets.Scripts.MenuAndUI;
using Assets.Scripts.Units.Character;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class ShopMenuController : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _balanceText;
        [SerializeField] private GameObject _dialogWindow;

        [SerializeField] private AudioSource _audioSourceExitSound;

        private void Awake()
        {
            _backButton.onClick.AddListener(() => _audioSourceExitSound.Play());
            _backButton.onClick.AddListener(() => Invoke("LoadMainMenu", 0.2f));

            _dialogWindow.SetActive(false);
            UpdateBalanceView();

            Observer.ShopDialogWindowActivationEvent += DialogWindowActivation;
            Observer.UpdateShopDataEvent += UpdateBalanceView;
        }

        private void OnDestroy()
        {
            Observer.ShopDialogWindowActivationEvent -= DialogWindowActivation;
            Observer.UpdateShopDataEvent -= UpdateBalanceView;
        }

        private void DialogWindowActivation(bool isActive)
        {
            if (isActive)
            {
                _dialogWindow.SetActive(true);
            }
            else
            {
                _dialogWindow.SetActive(false);
            }
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene((int)Scenes.MainMenu);
        }

        private void UpdateBalanceView()
        {
            _balanceText.text = $"Баланс: {CharacterAttributes.Coins}";
        }
    }
}
