using Assets.Scripts.Units.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Temporary
{
    internal class PenButtonController : MonoBehaviour
    {
        public Button button;

        private void Awake()
        {
            button= GetComponent<Button>();
            button.onClick.AddListener(GetPen);
        }

        private void GetPen()
        {
            Observer.ChangeWeaponEvent.Invoke(Units.Character.Attack.CharacterWeaponType.Pen);
        }
    }
}
