using DG.Tweening;
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
        private GameObject suitcase;
        private bool hasDrugs;

        private SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            sr.sprite = personData.GetRandomSprite();
            passport = personData.GetRandomPassport();
            suitcase = personData.GetRandomSuitcase();
            hasDrugs = Random.Range(0f, 1f) < personData.drugChance;
        }

        public void Die()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOShakePosition(0.5f));
            sequence.Append(transform.DOMoveY(-10, 1f)).SetEase(Ease.Linear);

            sequence.OnComplete(() => Destroy(gameObject));
        }

        public GameObject GetPassport() { return passport; }
        public GameObject GetSuitcase() { return suitcase; }
        public bool GetDrugs() { return hasDrugs; }
    }
}