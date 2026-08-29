using System;
using UnityEngine;

namespace PersonObjects
{
    public class SuitcaseBehaviour : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D suitcaseBounds;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Bounds GetBounds() { return suitcaseBounds.bounds; }
        public void SetSprite(Sprite sprite) { spriteRenderer.sprite = sprite; }
    }
}