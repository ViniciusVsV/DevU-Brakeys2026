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
        public float deathMovementDuration;

        [Header("Person Speaking")]
        [Header("Introduction Lines")]
        [Range(0, 1)] public float introductionSpeakingProbability;
        public string[] possibleIntrocutionLines;
        public float dialogueTypingDelay;
        public float dialogueFadeDelay;
        public float dialogueFadeDuration;

        public string GetRandomIntroductionLine() { return possibleIntrocutionLines[Random.Range(0, possibleIntrocutionLines.Length)]; }

        [Header("Death Lines")]
        [Range(0, 1)] public float deathSpeakingProbability;
        public string[] possibleDeathLines;

        public string GetRandomDeathLine() { return possibleDeathLines[Random.Range(0, possibleDeathLines.Length)]; }

        [Header("Person Building")]
        [Header("Sprites")]
        public Sprite[] possibleSprites;
        public Vector2[] photosPositionsInPassport;

        public int GetRandomSpriteIndex() { return Random.Range(0, possibleSprites.Length); }

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
        public Sprite[] possibleSuitcaseSprites;
        [Range(0, 1)] public float invalidItemProbability;
        public int minNumberItems;
        public int maxNumberItems;
        public GameObject[] possibleItems;
        public GameObject[] possibleInvalidItems;

        public Sprite GetRandomSuitcaseSprite() { return possibleSuitcaseSprites[Random.Range(0, possibleSuitcaseSprites.Length)]; }
        public GameObject GetRandomItem() { return possibleItems[Random.Range(0, possibleItems.Length)]; }
        public GameObject GetRandomInvalidItem() { return possibleInvalidItems[Random.Range(0, possibleInvalidItems.Length)]; }
    }
}