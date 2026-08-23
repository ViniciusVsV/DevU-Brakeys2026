using System;
using UnityEngine;

namespace MenuSystem
{
    public class AudioController : MonoBehaviour
    {
        public static event Action<AudioSource, AudioClip> OnSoundPlayed;

        public void PlaySFX(AudioClip audioClip)
        {
            OnSoundPlayed?.Invoke(null, audioClip);
        }
    }
}