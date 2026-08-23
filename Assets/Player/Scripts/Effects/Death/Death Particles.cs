using UnityEngine;

namespace Player.Effects
{
    public class DeathParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private Transform playerTr;

        public void ApplyEffect()
        {
            ParticleSystem newPs = Instantiate(ps);
            newPs.transform.position = playerTr.position;

            newPs.Play();
        }
    }
}