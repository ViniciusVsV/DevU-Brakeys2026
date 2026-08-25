using UnityEngine;

namespace Person
{
    [CreateAssetMenu(fileName = "PersonData", menuName = "Scriptable Objects/PersonData")]
    public class PersonData : ScriptableObject
    {
        public float spawnCooldown;

        public Sprite[] possibleSprites;
        public GameObject[] possiblePassports;
        public GameObject[] possibleSuitcases;
        [Range(0, 1)] public float drugChance;

        public Sprite GetRandomSprite() { return possibleSprites[Random.Range(0, possibleSprites.Length)]; }
        public GameObject GetRandomPassport() { return possiblePassports[Random.Range(0, possiblePassports.Length)]; }
    }
}