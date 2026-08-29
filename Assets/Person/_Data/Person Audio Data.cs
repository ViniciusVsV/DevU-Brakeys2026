using UnityEngine;

namespace Person
{
    [CreateAssetMenu(fileName = "PersonAudioData", menuName = "Scriptable Objects/PersonAudioData")]
    public class PersonAudioData : ScriptableObject
    {
        public AudioClip[] dialogueTypingSFXs;

        public AudioClip GetRandomTypingSFX() { return dialogueTypingSFXs[Random.Range(0, dialogueTypingSFXs.Length)]; }
    }
}