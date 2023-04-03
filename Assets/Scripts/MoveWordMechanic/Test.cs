using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxProject
{
    public class Test : MonoBehaviour
    {
        public WordType TypeForWords;
        private bool _mouseOnPanel;

        private void OnMouseEnter()
        {
            Debug.Log("Mouse enter");
            _mouseOnPanel = true;
        }

        private void OnMouseExit()
        {
            _mouseOnPanel = false;
        }

        private void Update()
        {
            if (!_mouseOnPanel)
                return;

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log($"Point up in panel with {TypeForWords} words");
            }
        }
    }
}