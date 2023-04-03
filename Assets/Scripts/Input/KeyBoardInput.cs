using System;
using UnityEngine;
using UEInput = UnityEngine.Input;

namespace GameBoxProject
{
    public class KeyBoardInput : MonoBehaviour, IInputSystem
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        public Vector2 Input { get; private set; }

        public bool Enabled { get; private set; }

        public Vector2 MousePosition { get; private set; }

        public event Action OnEscPressed;

        public void Disable() =>
            Enabled = false;
        public void Enable() =>
            Enabled = true;

        private void Update()
        {
            if (Enabled is false)
                return;

            if (UEInput.GetKeyDown(KeyCode.Escape))
                OnEscPressed?.Invoke();

            float x = UEInput.GetAxis(HORIZONTAL);
            float y = UEInput.GetAxis(VERTICAL);

            Input = new Vector2(x, y);
            MousePosition = UEInput.mousePosition;
        }
    }
}