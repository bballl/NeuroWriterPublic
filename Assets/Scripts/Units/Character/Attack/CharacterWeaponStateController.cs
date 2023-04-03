using UnityEngine;

namespace Assets.Scripts.Units.Character.Attack
{
    /// <summary>
    /// Контроллер смены оружия персонажа.
    /// </summary>
    internal sealed class CharacterWeaponStateController
    {
        private GameObject _pencil;
        private GameObject _pen;
        private GameObject _activeWeapon; //временно
        private Transform _weaponPosition;

        internal CharacterWeaponStateController(Transform weaponPosition)
        {
            _pencil = Resources.Load<GameObject>("Pencil");
            _pen = Resources.Load<GameObject>("Pen");
            _weaponPosition = weaponPosition;

            Observer.ChangeWeaponEvent += ChangeWeapon;
            Observer.GameLevelFinishedEvent += Unsubscribe;
            
            GetWeapon();
        }

        private void Unsubscribe()
        {
            Observer.ChangeWeaponEvent -= ChangeWeapon;
            Observer.GameLevelFinishedEvent -= Unsubscribe;
        }


        private void GetWeapon()
        {
            if (CharacterAttributes.CurrentWeapon == CharacterWeaponType.Pencil)
            {
                _activeWeapon = GameObject.Instantiate(_pencil, _weaponPosition);
            }
            else
                _activeWeapon = GameObject.Instantiate(_pen, _weaponPosition);
        }

        private void ChangeWeapon(CharacterWeaponType type)
        {
            GameObject.Destroy(_activeWeapon);

            if (type == CharacterWeaponType.Pencil)
                _activeWeapon = GameObject.Instantiate(_pencil, _weaponPosition);

            if (type == CharacterWeaponType.Pen)
                _activeWeapon = GameObject.Instantiate(_pen, _weaponPosition);
        }
    }
}
