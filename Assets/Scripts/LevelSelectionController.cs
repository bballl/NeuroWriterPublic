using Assets.Scripts.Units.Character.Attack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameBoxProject
{
    public class LevelSelectionController : MonoBehaviour
    {
        [SerializeField] BlockButtonIcons _penButton;
        [SerializeField] BlockButtonIcons _pencilButton;

        [SerializeField] 

        private Dictionary<CharacterWeaponType, Button> _weaponButtons = new();
        private List<BlockButtonIcons> _levelList = new();

        private void Start()
        {
            MouseObserver.OnButtonClicked += MouseObserver_OnButtonClicked;
        }

        private void MouseObserver_OnButtonClicked()
        {
            throw new System.NotImplementedException();
        }
    }
}