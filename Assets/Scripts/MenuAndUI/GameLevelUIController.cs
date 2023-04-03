using Assets.Scripts.Units.Character;
using Assets.Scripts.Units.Character.Ability;
using Assets.Scripts.Units.Character.Attack;
using GameBoxProject;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MenuAndUI
{
    internal class GameLevelUIController : MonoBehaviour
    {
        [SerializeField] Button _exitButton;
        
        [SerializeField] Button _simpleAttackButton;
        [SerializeField] Button _impulseAttackButton;
        [SerializeField] Button _sectoralAttackButton;
        
        [SerializeField] Button _extraDamageButton;
        [SerializeField] Button _invulnerAbilityButton;
        [SerializeField] Button _droneAbilityButton;
        
        [SerializeField] Button _pencilButton;
        [SerializeField] Button _penButton;

        private void Awake()
        {
            _exitButton.onClick.AddListener(() => ScreenFader.Instance.Activate(() => SceneManager.LoadScene((int)Scenes.MainMenu)));
            
            //_simpleAttackButton.onClick.AddListener(() => ChangeAttackType(PencilAttackType.SimpleAttack));
            //_impulseAttackButton.onClick.AddListener(() => ChangeAttackType(PencilAttackType.ImpulseAttack));
            //_sectoralAttackButton.onClick.AddListener(() => ChangeAttackType(PencilAttackType.SectoralAttack));

            _extraDamageButton.onClick.AddListener(() => ApplyAbility(AbilityType.ExtraDamageAbility));
            _invulnerAbilityButton.onClick.AddListener(() => ApplyAbility(AbilityType.InvulnerAbility));
            _droneAbilityButton.onClick.AddListener(() => ApplyAbility(AbilityType.DroneAbility));
            
            _pencilButton.onClick.AddListener(() => ChangeWeapon(CharacterWeaponType.Pencil));
            _penButton.onClick.AddListener(() => ChangeWeapon(CharacterWeaponType.Pen));
        }

        //private void ChangeAttackType(PencilAttackType type)
        //{
        //    Observer.ChangePencilAttackTypeEvent.Invoke(type);
        //}

        private void ChangeWeapon(CharacterWeaponType type)
        {
            Observer.ChangeWeaponEvent.Invoke(type);
        }

        private void ApplyAbility(AbilityType type)
        {
            Observer.ActivationAbilityEvent.Invoke(type);
        }
        
        private void Quit()
        {
            Application.Quit();
            //SceneManager.LoadScene((int)Scenes.MainMenu);
        }
    }
}
