using System;
using DG.Tweening;
using PersonObjects;
using UnityEngine;

namespace Person
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PersonBehaviour : MonoBehaviour
    {
        //Cada pessoa vai ter uma mala e um passaporte aleatórios das listas
        //Cada pessoa aleatoriamente terá drogas ou não
        [SerializeField] private PersonData personData;

        private GameObject carriedPassport;
        private GameObject referencePassport;
        private GameObject suitcase;

        private bool hasDrugs;
        public bool isInvalid;

        private SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            hasDrugs = UnityEngine.Random.Range(0f, 1f) < personData.drugChance;
            isInvalid = hasDrugs;
        }

        public void Move(Vector2 destination, Action onFinish)
        {
            transform.DOMove(destination, personData.movementDuration)
                .SetEase(personData.movementEase)
                .OnComplete(() => { onFinish?.Invoke(); });
        }

        public void Die()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOShakePosition(0.3f));
            sequence.Append(transform.DOMoveY(-100, 1f)).SetEase(Ease.Linear);

            sequence.OnComplete(() => Destroy(gameObject));
        }

        public void SetReferencePassport(GameObject passport) { referencePassport = passport; }
        public void SetCarriedPassport(GameObject passport) { carriedPassport = passport; }
        public void SetSuitcase(GameObject suitcase) { this.suitcase = suitcase; }

        public GameObject GetCarriedPassport() { return carriedPassport; }
        public GameObject GetReferencePassport() { return referencePassport; }
        public GameObject GetSuitcase() { return suitcase; }
        public bool GetDrugs() { return hasDrugs; }
    }
}