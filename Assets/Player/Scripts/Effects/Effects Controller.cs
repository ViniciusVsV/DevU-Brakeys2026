using System;
using System.Collections;
using Player.Effects;
using UnityEngine;

namespace Player
{
    public class EffectsController : MonoBehaviour
    {
        [Header("Player Objects")]
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private SpriteRenderer playerSr;

        [Header("Player Scripts")]
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private SpriteController spriteController;
        [SerializeField] private AudioController audioController;

        [Header("Generic Effects")]
        [SerializeField] private ControllerRumble controllerRumble;
        [SerializeField] private MovementParticles movementParticles;

        [Header("Dash Effects")]
        [SerializeField] private AfterImagesManager afterImagesManager;

        [Header("Death Effects")]
        [SerializeField] private DeathCameraShake deathCameraShake;
        [SerializeField] private DeathParticles deathParticles;
        [SerializeField] private DeathKnockback deathKnockback;
        [SerializeField] private DeathTimeSlow deathTimeSlow;
        [SerializeField] private SpriteFlash spriteFlash;

        public void PlayWalkEffects(bool isGrounded)
        {
            if (isGrounded && Mathf.Abs(playerRb.linearVelocityX) > 0.01f)
                movementParticles.ToggleRunparticles(true);
            else
                movementParticles.ToggleRunparticles(false);
        }

        public void PlayJumpEffects()
        {
            movementParticles.PlayJumpParticles();

            audioController.PlayJumpSFX();
        }

        public void ToggleDashEffects(bool activate)
        {
            if (activate)
            {
                afterImagesManager.StartAfterImages(playerSr.transform, playerSr);

                audioController.PlayDashSFX();
            }
            else
                afterImagesManager.StopAfterImages(playerSr.transform);
        }

        public void PlayDeathEffects(Vector3 collisionDirection, Action onFinish)
        {
            spriteController.TriggerDeathAnimation();
            audioController.PlayDeathSFX();

            playerRb.simulated = false;

            StartCoroutine(DeathEffectsRoutine(collisionDirection, onFinish));
        }
        private IEnumerator DeathEffectsRoutine(Vector3 collisionDirection, Action onFinish)
        {
            //Pisca o sprite
            spriteFlash.ApplyEffect(playerSr);

            //Treme a câmera
            deathCameraShake.ApplyEffect();

            //Aplica tremor do controle
            if (inputHandler.isOnController)
                controllerRumble.ApplyEffect();

            //Faz o animator ser unscaledTime
            spriteController.SetUnscaledTime();

            //Ativa time slow
            deathTimeSlow.ApplyEffect();

            //Aplica knockback
            deathKnockback.ApplyEffect(-collisionDirection);

            //Espera o knockback acabar
            yield return new WaitUntil(() => deathKnockback.finished == true);

            //Desativa o sprite
            spriteController.DisableSprite();

            //Ivoca as partículas de morte
            deathParticles.ApplyEffect();

            onFinish?.Invoke();
        }
    }
}