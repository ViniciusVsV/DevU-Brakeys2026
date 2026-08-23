using DG.Tweening;
using UnityEngine;

namespace Player.Effects
{
    public class DeathKnockback : MonoBehaviour
    {
        [SerializeField] private PlayerEffectsData playerEffectsData;
        [SerializeField] private Transform playerTr;
        [SerializeField] private LayerMask groundLayer;

        public bool finished;

        public void ApplyEffect(Vector3 knockbackDirection)
        {
            finished = false;

            RaycastHit2D hit = Physics2D.Raycast(playerTr.position, knockbackDirection, playerEffectsData.deathKnockbackDistance, groundLayer);

            Vector3 endPos;

            //Se há uma parede no caminho, para antes
            if (hit.collider != null)
                endPos = hit.point - (Vector2)knockbackDirection * playerEffectsData.wallDetectedOffset;
            else
                endPos = playerTr.position + playerEffectsData.deathKnockbackDistance * knockbackDirection;

            playerTr.DOMove(endPos, playerEffectsData.deathKnockbackDuration)
            .SetEase(playerEffectsData.deathKnockbackEase)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                finished = true;
            });
        }
    }
}