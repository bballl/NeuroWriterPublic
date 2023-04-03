using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxProject
{
    public interface IInputSystem
    {
        public event Action OnEscPressed;

        Vector2 Input { get; }
        Vector2 MousePosition { get; }

        bool Enabled { get; }

        void Enable();
        void Disable();
    }
}