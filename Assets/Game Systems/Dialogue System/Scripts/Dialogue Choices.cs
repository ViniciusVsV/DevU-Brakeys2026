using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueChoices : MonoBehaviour
    {
        [SerializeField] private DialogueManager dialogueManager;

        [SerializeField] private GameObject choicesPanel;
        [SerializeField] private GameObject[] choicesObjects;
        private TextMeshProUGUI[] choicesTexts;
        //[SerializeField] private ChoiceEffects choiceEffects;

        private void Awake()
        {
            choicesTexts = new TextMeshProUGUI[choicesObjects.Length];
            for (int i = 0; i < choicesObjects.Length; i++)
                choicesTexts[i] = choicesObjects[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        public void DisplayChoices()
        {
            Story aux = dialogueManager.currentStory;
            List<Choice> currentChoices = aux.currentChoices;

            if (currentChoices.Count == 0)
                return;

            choicesPanel.SetActive(true);

            for (int i = 0; i < currentChoices.Count; i++)
            {
                choicesTexts[i].text = currentChoices[i].text;
                choicesObjects[i].SetActive(true);
            }
        }

        public void HideChoices()
        {
            foreach (var obj in choicesObjects)
                obj.SetActive(false);

            choicesPanel.SetActive(false);
        }

        public void MakeChoice(int index)
        {
            dialogueManager.currentStory.ChooseChoiceIndex(index);

            HideChoices();

            dialogueManager.ContinueDialogue();
        }
    }
}