using System;
using UnityEngine;

namespace Minigames
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private MinigamesAudioData minigamesAudioData;
        [SerializeField] private AudioSource audioSource;

        public static event Action<AudioClip, AudioSource> OnSoundPlay;
        public static event Action<AudioClip, AudioSource> OnContinuousSFXPlay;
        public static event Action<AudioSource> OnContinuousSFXStop;

        public void PlayTVStatic() { OnSoundPlay?.Invoke(minigamesAudioData.tvTurnOnSFX, audioSource); }

        public void PlaySuitcaseFallSFX() { OnSoundPlay?.Invoke(minigamesAudioData.suitcaseFallSFX, audioSource); }
        public void StartBeltSFX() { OnContinuousSFXPlay?.Invoke(minigamesAudioData.beltSFX, audioSource); }
        public void StopBeltSFX() { OnContinuousSFXStop?.Invoke(audioSource); }

        public void StartSniffingSFX() { OnContinuousSFXPlay?.Invoke(minigamesAudioData.sniffingSFX, audioSource); }
        public void StopSniffingSFX() { OnContinuousSFXStop?.Invoke(audioSource); }
        public void PlayFinishSniffingSFX() { OnSoundPlay?.Invoke(minigamesAudioData.finishedSniffingSFX, audioSource); }
    }
}