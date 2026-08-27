using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action<int> OnSectionChosen;

        public void On1(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSectionChosen?.Invoke(1);
        }

        public void On2(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSectionChosen?.Invoke(2);
        }

        public void On3(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSectionChosen?.Invoke(3);
        }
    }
}