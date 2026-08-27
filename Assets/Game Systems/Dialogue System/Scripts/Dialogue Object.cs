using System;
using Ink.Runtime;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueObject : MonoBehaviour
    {
        [SerializeField] private TextAsset inkTextJSON;
        [SerializeField] private bool blockPlayerActions;
        public Story currentStory;

        public static event Action<Story, bool> OnDialogueInteracted;
        public static event Action OnDialogueKilled;

        private void Awake()
        {
            currentStory = new Story(inkTextJSON.text);
        }

        public void InteractDialogue()
        {
            OnDialogueInteracted?.Invoke(currentStory, blockPlayerActions);
        }

        public void KillDialogue()
        {
            OnDialogueKilled?.Invoke();
        }
    }
}