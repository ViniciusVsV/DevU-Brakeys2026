using UnityEngine;

namespace Player.Effects
{
    public class MovementParticles : MonoBehaviour
    {
        [SerializeField] private PlayerEffectsData playerEffectsData;
        [SerializeField] private ParticleSystem runParticles;
        [SerializeField] private ParticleSystem jumpParticles;
        private float runParticlesTimer;

        private bool runParticlesActive;

        private void Update()
        {
            if (!runParticlesActive)
                return;

            if (runParticlesTimer > Mathf.Epsilon)
                runParticlesTimer -= Time.deltaTime;
            else
            {
                runParticles.Play();
                runParticlesTimer = playerEffectsData.runParticlesCooldown;
            }
        }

        public void ToggleRunparticles(bool activate)
        {
            if (activate)
                runParticlesActive = true;
            else
                runParticlesActive = false;
        }

        public void PlayJumpParticles()
        {
            jumpParticles.Play();
        }
    }
}