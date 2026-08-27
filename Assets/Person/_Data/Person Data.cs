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

        [Header("Person Movement")]
        public float movementDuration;
        public Ease movementEase;

        [Header("Person Building")]
        [Header("Sprites")]
        public Sprite[] possibleSprites;
        public Sprite GetRandomSprite() { return possibleSprites[Random.Range(0, possibleSprites.Length)]; }

        [Header("Drugs")]
        [Range(0, 1)] public float drugChance;

        [Header("Passport Generation")]
        [Range(0, 1)] public float invalidPassportProbability;
        public string[] possibleNames;
        public string[] possibleCountries;
        public string[] possibleGenders;
        public string[] possibleInvalidNames;
        public string[] possibleInvalidCountries;
        public string[] possibleInvalidGenders;

        public int GetRandomNameIndex() { return Random.Range(0, possibleNames.Length); }
        public int GetRandomCountryIndex() { return Random.Range(0, possibleCountries.Length); }
        public int GetRandomGenderIndex() { return Random.Range(0, possibleGenders.Length); }

        [Header("Suitcase Generation")]
        [Range(0, 1)] public float invalidItemProbability;
        public int minNumberItems;
        public int maxNumberItems;
        public GameObject[] possibleItems;
        public GameObject[] possibleInvalidItems;

        public GameObject GetRandomItem() { return possibleItems[Random.Range(0, possibleItems.Length)]; }
        public GameObject GetRandomInvalidItem() { return possibleInvalidItems[Random.Range(0, possibleInvalidItems.Length)]; }
    }
}