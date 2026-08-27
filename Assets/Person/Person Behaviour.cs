using System;
using System.Collections;
using DG.Tweening;
using PersonObjects;
using TMPro;
using UnityEngine;

namespace Person
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PersonBehaviour : MonoBehaviour
    {
        //Cada pessoa vai ter uma mala e um passaporte aleatórios das listas
        //Cada pessoa aleatoriamente terá drogas ou não
        [SerializeField] private PersonData personData;

        private SpriteRenderer sr;
        private Sprite sprite;

        private GameObject carriedPassport;
        private GameObject referencePassport;
        private GameObject suitcase;

        [Header("Dialogue Line")]
        [SerializeField] private TextMeshProUGUI textUI;
        private string line;
        private bool hasSpoken;

        private bool hasDrugs;
        public bool isInvalid;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            sprite = personData.GetRandomSprite();
            sr.sprite = sprite;

            hasDrugs = UnityEngine.Random.Range(0f, 1f) < personData.drugChance;
            isInvalid = hasDrugs;

            line = personData.GetRandomLine();

            textUI.maxVisibleCharacters = 0;
            textUI.text = line;
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

        public void Speak()
        {
            if (line.Length == 0 || hasSpoken || UnityEngine.Random.Range(0f, 1f) > personData.speakingProbability)
                return;

            hasSpoken = true;

            textUI.transform.parent.SetParent(null);

            StartCoroutine(SpeakRoutine());
        }
        private IEnumerator SpeakRoutine()
        {
            textUI.ForceMeshUpdate();

            int totalCharacters = textUI.textInfo.characterCount;

            for (int i = 0; i <= totalCharacters; i++)
            {
                textUI.maxVisibleCharacters = i;

                yield return new WaitForSeconds(personData.dialogueTypingDelay);
            }

            textUI.DOFade(0f, personData.dialogueFadeDuration).SetDelay(personData.dialogueFadeDelay)
                .OnComplete(() =>
                {
                    Destroy(textUI.transform.parent.gameObject);
                });
        }

        public void SetReferencePassport(GameObject passport) { referencePassport = passport; }
        public void SetCarriedPassport(GameObject passport) { carriedPassport = passport; }
        public void SetSuitcase(GameObject suitcase) { this.suitcase = suitcase; }

        public Sprite GetSprite() { return sprite; }
        public GameObject GetCarriedPassport() { return carriedPassport; }
        public GameObject GetReferencePassport() { return referencePassport; }
        public GameObject GetSuitcase() { return suitcase; }
        public bool GetDrugs() { return hasDrugs; }
    }
}