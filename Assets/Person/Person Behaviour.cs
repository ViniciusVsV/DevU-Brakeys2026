using System;
using System.Collections;
using DG.Tweening;
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
        [SerializeField] private AudioController audioController;

        private SpriteRenderer sr;
        private Sprite sprite;

        private GameObject carriedPassport;
        private GameObject referencePassport;
        private GameObject suitcase;

        [Header("Dialogue Line")]
        [SerializeField] private TextMeshProUGUI textUI;
        private string introductionLine;
        private string deathLine;
        private bool hasSpokenIntroduction;
        private Coroutine speakRoutine;

        private bool hasDrugs;
        public bool isInvalid;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();

            sprite = personData.GetRandomSprite();
            sr.sprite = sprite;

            hasDrugs = UnityEngine.Random.Range(0f, 1f) < personData.drugChance;
            isInvalid = hasDrugs;

            introductionLine = personData.GetRandomIntroductionLine();
            deathLine = personData.GetRandomDeathLine();

            textUI.maxVisibleCharacters = 0;
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
            sequence.Append(transform.DOMoveY(transform.position.y - 20f, personData.deathMovementDuration)).SetEase(Ease.Linear);

            sequence.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public void Speak(Transform dialoguePoint, bool isDeathLine)
        {
            //Se já falou a fala introdutória e é uma fala introdutória
            if (hasSpokenIntroduction && !isDeathLine)
                return;

            //Rola as probabilidades de falar cada tipo de fala
            float randomRoll = UnityEngine.Random.Range(0f, 1f);

            if (isDeathLine && randomRoll > personData.deathSpeakingProbability)
                return;

            if (!isDeathLine && randomRoll > personData.introductionSpeakingProbability)
                return;

            hasSpokenIntroduction = !isDeathLine;

            textUI.transform.parent.SetParent(null);
            textUI.transform.position = dialoguePoint.position;

            textUI.text = isDeathLine ? deathLine : introductionLine;

            textUI.maxVisibleCharacters = 0;
            textUI.DOKill();
            textUI.DOFade(1f, 0f);

            if (speakRoutine != null)
                StopCoroutine(speakRoutine);

            speakRoutine = StartCoroutine(SpeakRoutine(isDeathLine));
        }
        private IEnumerator SpeakRoutine(bool isDeathLine)
        {
            textUI.ForceMeshUpdate();

            int totalCharacters = textUI.textInfo.characterCount;

            for (int i = 0; i <= totalCharacters; i++)
            {
                textUI.maxVisibleCharacters = i;

                audioController.PlayTypingSFX();

                yield return new WaitForSeconds(personData.dialogueTypingDelay);
            }

            textUI.DOFade(0f, personData.dialogueFadeDuration).SetDelay(personData.dialogueFadeDelay)
                .OnComplete(() =>
                {
                    if (isDeathLine)
                        Destroy(textUI.transform.parent.gameObject);
                });

            speakRoutine = null;
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