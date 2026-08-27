using System;
using System.Collections;
using DG.Tweening;
using DialogueSystem;
using Person;
using Sections;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private PersonBehaviour tutorialGuyPrefab;
        [SerializeField] private Transform firstSectionActivePoint;
        [SerializeField] private string gameSceneName;

        [Header("Highlight")]
        [SerializeField] private SpriteRenderer highlightScreen;
        [SerializeField] private GameObject sectionsHighlight;
        [SerializeField] private GameObject approveHighlight;
        [SerializeField] private GameObject rulesHighlight;
        private Tween highlightScreenTween;

        private PersonBehaviour tutorialGuy;
        private DialogueObject tutorialGuyDialogue;

        public bool dialogueFinished;

        public static event Action<string> OnTutorialFinish;

        private void Start()
        {
            tutorialGuy = Instantiate(tutorialGuyPrefab, transform.position, Quaternion.identity);

            tutorialGuyDialogue = tutorialGuy.GetComponentInChildren<DialogueObject>();

            tutorialGuyDialogue.currentStory.BindExternalFunction("HighlightSectionButtons", () =>
            {
                sectionsHighlight.SetActive(true);
                highlightScreenTween = highlightScreen.DOFade(0.6f, 1f);
            });
            tutorialGuyDialogue.currentStory.BindExternalFunction("HighlightApproveButtons", () =>
            {
                highlightScreenTween.Complete();
                sectionsHighlight.SetActive(false);
                approveHighlight.SetActive(true);
            });
            tutorialGuyDialogue.currentStory.BindExternalFunction("HighlightRulesButton", () =>
            {
                approveHighlight.SetActive(false);
                rulesHighlight.SetActive(true);
            });
            tutorialGuyDialogue.currentStory.BindExternalFunction("DisableHighlights", () =>
            {
                rulesHighlight.SetActive(false);
                highlightScreenTween = highlightScreen.DOFade(0f, 1f);
            });
            tutorialGuyDialogue.currentStory.BindExternalFunction("FinishDialogue", () =>
            {
                highlightScreenTween.Complete();

                tutorialGuyDialogue.KillDialogue();
                dialogueFinished = true;
            });

            StartCoroutine(TutorialRoutine());
        }

        private IEnumerator TutorialRoutine()
        {
            yield return new WaitForSeconds(2f);

            bool finished = false;

            tutorialGuy.Move(firstSectionActivePoint.position, () => { finished = true; });

            yield return new WaitUntil(() => finished);

            tutorialGuyDialogue.InteractDialogue();

            yield return new WaitUntil(() => dialogueFinished);

            OnTutorialFinish?.Invoke(gameSceneName);
        }
    }
}