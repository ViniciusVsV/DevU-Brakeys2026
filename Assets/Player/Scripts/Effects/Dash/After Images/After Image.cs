using System.Collections;
using UnityEngine;

namespace Player.Effects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AfterImage : MonoBehaviour
    {
        [SerializeField] private PlayerEffectsData playerEffectsData;

        private AfterImagesManager afterImagesManager;
        private SpriteRenderer spriteRenderer;
        private Color startingColor;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            startingColor = Color.white;
            startingColor.a = playerEffectsData.startingAlpha;
        }

        public void Initialize(AfterImagesManager afterImagesManager)
        {
            this.afterImagesManager = afterImagesManager;
        }

        public void ApplyEffect(Vector2 position, Sprite sprite)
        {
            transform.position = position;

            spriteRenderer.sprite = sprite;
            spriteRenderer.color = startingColor;

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            float elapsedTime = 0;
            float progress;

            Color color = startingColor;

            while (elapsedTime < playerEffectsData.imageDuration)
            {
                progress = elapsedTime / playerEffectsData.imageDuration;

                color.a = Mathf.Lerp(playerEffectsData.startingAlpha, 0, progress);
                spriteRenderer.color = color;

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            spriteRenderer.color = Color.clear;
            spriteRenderer.sprite = null;

            afterImagesManager.ReturnImage(this);
        }
    }
}