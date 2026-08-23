using System;
using System.Linq;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class DeathDetector : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviourData playerBehaviourData;

        [Header("Player Scripts")]
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private EffectsController effectsController;

        private Collider2D col;

        public static event Action OnPlayerDeathDetected;
        public static event Action OnPlayerDeathCompleted;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (playerBehaviourData.hostileTags.Contains(collision.tag))
            {
                col.enabled = false;

                StartDeath(collision);
            }
        }

        private void StartDeath(Collider2D collision)
        {
            OnPlayerDeathDetected?.Invoke();

            inputHandler.DisableInputs();

            Vector3 closestPoint = collision.ClosestPoint(transform.position);
            Vector3 colDirection = (closestPoint - transform.position).normalized;

            effectsController.PlayDeathEffects(colDirection, FinishDeath);
        }

        private void FinishDeath()
        {
            OnPlayerDeathCompleted?.Invoke();
        }
    }
}