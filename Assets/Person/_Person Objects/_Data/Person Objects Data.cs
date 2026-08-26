using UnityEngine;

namespace PersonObjects
{
    [CreateAssetMenu(fileName = "PersonObjectsData", menuName = "Scriptable Objects/PersonObjectsData")]
    public class PersonObjectsData : ScriptableObject
    {
        [Header("Passport")]
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

        [Header("Suitcase")]
        [Range(0, 1)] public float invalidItemProbability;
        public int minNumberItems;
        public int maxNumberItems;
        public GameObject[] possibleItems;
        public GameObject[] possibleInvalidItems;

        public GameObject GetRandomItem() { return possibleItems[Random.Range(0, possibleItems.Length)]; }
        public GameObject GetRandomInvalidItem() { return possibleInvalidItems[Random.Range(0, possibleInvalidItems.Length)]; }
    }
}