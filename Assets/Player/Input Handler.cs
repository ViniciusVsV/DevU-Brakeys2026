using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public bool pauseDisabled;
        private bool isPaused;

        public static event Action<bool> OnPausePress;

        public void OnPause(InputAction.CallbackContext context)
        {
            if (pauseDisabled)
                return;

            if (context.performed)
            {
                if (isPaused)
                    isPaused = false;
                else
                    isPaused = true;

                OnPausePress?.Invoke(isPaused);
            }
        }
    }
}