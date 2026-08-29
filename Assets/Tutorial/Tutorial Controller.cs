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
        [SerializeField] private AudioClip tutorialMusic;

        [SerializeField] private PersonBehaviour tutorialGuyPrefab;
        [SerializeField] private Transform tutorialGuySpawnPoint;
        [SerializeField] private Transform firstSectionActivePoint;
        [SerializeField] private string gameSceneName;

        [Header("Highlight")]
        [SerializeField] private SpriteRenderer highlightScreen;
        [SerializeField] private GameObject sectionsHighlight;
        [SerializeField] private GameObject approveHighlight;
        [SerializeField] private GameObject rulesHighlight;
        [SerializeField] private GameObject counterHighlight;
        private Tween highlightScreenTween;

        private PersonBehaviour tutorialGuy;
        private DialogueObject tutorialGuyDialogue;

        public bool dialogueFinished;

        public static event Action<AudioClip> OnMusicPlay;
        public static event Action<bool> OnMusicFade;
        public static event Action<string> OnSceneExit;

        private void Start()
        {
            //Botar para tocar a música do tutorial
            OnMusicPlay?.Invoke(tutorialMusic);

            tutorialGuy = Instantiate(tutorialGuyPrefab, tutorialGuySpawnPoint.position, Quaternion.identity);

            tutorialGuyDialogue = tutorialGuy.GetComponentInChildren<DialogueObject>();

            tutorialGuyDialogue.currentStory.BindExternalFunction("HighlightSectionButtons", () =>
            {
                sectionsHighlight.SetActive(true);
                highlightScreenTween = highlightScreen.DOFade(0.8f, 1f);
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
            tutorialGuyDialogue.currentStory.BindExternalFunction("HighlightLineCounter", () =>
            {
                rulesHighlight.SetActive(false);
                counterHighlight.SetActive(true);
            });
            tutorialGuyDialogue.currentStory.BindExternalFunction("DisableHighlights", () =>
            {
                counterHighlight.SetActive(false);
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
            yield return new WaitForSeconds(3f);

            bool finished = false;

            tutorialGuy.Move(firstSectionActivePoint.position, () => { finished = true; });

            yield return new WaitUntil(() => finished);

            tutorialGuyDialogue.InteractDialogue();

            yield return new WaitUntil(() => dialogueFinished);

            //Dar um fade out na música do tutorial
            OnMusicFade?.Invoke(true);

            yield return new WaitForSeconds(2f);

            OnSceneExit?.Invoke(gameSceneName);
        }
    }
}