using System;
using UnityEngine;

namespace Person
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private PersonAudioData personAudioData;
        [SerializeField] private AudioSource audioSource;

        public static event Action<AudioClip, AudioSource> OnSoundPlay;

        public void PlayTypingSFX() { OnSoundPlay?.Invoke(personAudioData.GetRandomTypingSFX(), audioSource); }
    }
}