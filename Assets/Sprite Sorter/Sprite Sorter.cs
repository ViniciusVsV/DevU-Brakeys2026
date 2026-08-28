using UnityEngine;

namespace Scenario
{
    public class SpriteSorter : MonoBehaviour
    {
        [SerializeField] private string dynamicSortingLayer;
        [SerializeField] private SpriteRenderer[] sequentialRenderers;
        private SpriteRenderer sr;

        private void Awake()
        {
            if (sequentialRenderers.Length != 0)
            {
                foreach (var renderer in sequentialRenderers)
                    renderer.sortingLayerName = dynamicSortingLayer;

                return;
            }

            sr = GetComponentInParent<SpriteRenderer>();

            if (sr != null)
                sr.sortingLayerName = dynamicSortingLayer;
        }

        private void LateUpdate()
        {
            if (sequentialRenderers.Length != 0)
            {
                for (int i = 0; i < sequentialRenderers.Length; i++)
                    sequentialRenderers[i].sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + i;

                return;
            }

            if (sr != null)
                sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}