using System;
using UnityEngine;

namespace PersonObjects
{
    public class SuitcaseBehaviour : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D suitcaseBounds;

        public Bounds GetBounds() { return suitcaseBounds.bounds; }
    }
}