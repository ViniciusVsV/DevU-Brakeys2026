using DG.Tweening;
using UnityEngine;

namespace Player.Effects
{
    public class SpriteFlash : MonoBehaviour
    {
        [SerializeField] private PlayerEffectsData playerEffectsData;

        private MaterialPropertyBlock propertyBlock;
        private Tween tween;

        private void Awake()
        {
            propertyBlock = new MaterialPropertyBlock();
        }

        public void ApplyEffect(SpriteRenderer spriteRenderer)
        {
            tween?.Kill();

            spriteRenderer.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor("_Color", playerEffectsData.flashColor);
            propertyBlock.SetFloat("_Intensity", 1f);

            spriteRenderer.SetPropertyBlock(propertyBlock);

            tween = DOTween.To(
                () => 1f,
                value =>
                {
                    spriteRenderer.GetPropertyBlock(propertyBlock);

                    propertyBlock.SetFloat("_Intensity", value);

                    spriteRenderer.SetPropertyBlock(propertyBlock);
                },
                0f,
                playerEffectsData.flashDuration
            )
            .SetEase(playerEffectsData.flashEase)
            .SetUpdate(true);
        }
    }
}