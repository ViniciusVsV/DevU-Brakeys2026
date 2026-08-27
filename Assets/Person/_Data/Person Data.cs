using DG.Tweening;
using UnityEngine;

namespace Person
{
    [CreateAssetMenu(fileName = "PersonData", menuName = "Scriptable Objects/PersonData")]
    public class PersonData : ScriptableObject
    {
        [Header("Person Spawning")]
        public float initialDelay;
        public float initialSpawnCooldown;
        public float timeToReachMaxDificulty;
        public float finalSpawnCooldown;
        public AnimationCurve dificultyCurve;

        [Header("Person Building")]
        public Sprite[] possibleSprites;
        [Range(0, 1)] public float drugChance;

        [Header("Person Movement")]
        public float movementDuration;
        public Ease movementEase;

        public Sprite GetRandomSprite()
        {
            return possibleSprites[Random.Range(0, possibleSprites.Length)];
        }
    }
}