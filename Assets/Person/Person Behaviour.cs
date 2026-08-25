using UnityEngine;

namespace Person
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PersonBehaviour : MonoBehaviour
    {
        //Cada pessoa vai ter uma mala e um passaporte aleatórios das listas
        //Cada pessoa aleatoriamente terá drogas ou não
        [SerializeField] private PersonData personData;

        private GameObject passport;
        private bool hasDrugs;

        private SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            sr.sprite = personData.GetRandomSprite();
            passport = personData.GetRandomPassport();
            hasDrugs = Random.Range(0f, 1f) < personData.drugChance;
        }

        public GameObject GetPassport() { return passport; }
        public bool GetDrugs() { return hasDrugs; }
    }
}