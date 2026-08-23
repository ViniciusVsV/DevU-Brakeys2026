using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviourData playerBehaviourData;
        [SerializeField] private PlayerBehaviour playerBehaviour;

        private float dashCooldownTimer;

        [Header("Booleans")]
        public bool inputsDisabled;
        public bool movementDisabled;
        public bool jumpDisabled;
        public bool dashDisabled;
        public bool pauseDisabled;
        public bool isPaused;
        public bool isOnController;

        public static event Action<bool> OnPausePressed;

        private void Update()
        {
            if (dashCooldownTimer > Mathf.Epsilon)
                dashCooldownTimer -= Time.deltaTime;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (inputsDisabled || movementDisabled)
            {
                playerBehaviour.Move(0);
                return;
            }

            Vector2 moveDirection = context.ReadValue<Vector2>();
            moveDirection = moveDirection.normalized;

            if (Mathf.Abs(moveDirection.x) < 0.2f)
            {
                playerBehaviour.Move(0);
                return;
            }

            playerBehaviour.Move((int)Mathf.Sign(moveDirection.x));
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (inputsDisabled || jumpDisabled)
                return;

            if (context.performed)
                playerBehaviour.BufferJump();
            else
                playerBehaviour.JumpCut();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (playerBehaviour.canDash & dashCooldownTimer <= Mathf.Epsilon & context.performed)
            {
                dashCooldownTimer = playerBehaviourData.dashCooldown;
                StartCoroutine(playerBehaviour.Dash());
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (pauseDisabled)
                return;

            if (context.performed)
            {
                if (isPaused)
                {
                    inputsDisabled = false;
                    isPaused = false;
                }
                else
                {
                    inputsDisabled = true;
                    isPaused = true;

                    playerBehaviour.Move(0);
                }

                OnPausePressed?.Invoke(isPaused);
            }
        }

        public void CheckForController(InputAction.CallbackContext context)
        {
            if (context.control.device is Gamepad)
            {
                isOnController = true;
                Cursor.visible = false;
            }
            else
            {
                isOnController = false;
                Cursor.visible = true;
            }
        }

        public void EnableInputs()
        {
            inputsDisabled = false;
            pauseDisabled = false;
        }
        public void DisableInputs()
        {
            if (inputsDisabled)
                return;

            inputsDisabled = true;
            pauseDisabled = true;

            playerBehaviour.Move(0);
        }
    }
}