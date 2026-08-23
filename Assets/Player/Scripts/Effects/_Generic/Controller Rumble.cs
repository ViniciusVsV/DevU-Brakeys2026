using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Effects
{
    public class ControllerRumble : MonoBehaviour
    {
        [SerializeField] private PlayerEffectsData playerEffectsData;

        private Gamepad gamepad;
        private Coroutine coroutine;


        public void ApplyEffect()
        {
            gamepad = Gamepad.current;

            if (gamepad != null)
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);

                coroutine = StartCoroutine(Routine(playerEffectsData.deathLowFrequency, playerEffectsData.deathHighFrequency, playerEffectsData.deathRumbleDuration));
            }
        }

        private IEnumerator Routine(float lowFrequency, float highFrequency, float duration)
        {
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

            yield return new WaitForSeconds(duration);

            gamepad.SetMotorSpeeds(0f, 0f);
        }
    }
}