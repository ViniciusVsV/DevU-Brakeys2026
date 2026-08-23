using System;
using UnityEngine;

namespace Player
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private PlayerAudioData playerAudioData;

        public static event Action<AudioSource, AudioClip> OnSoundPlayed;

        public void PlayWalkSFX()
        {
            AudioClip[] sfxs = playerAudioData.walkSFXs;

            OnSoundPlayed?.Invoke(null, sfxs[UnityEngine.Random.Range(0, sfxs.Length)]);
        }

        public void PlayJumpSFX()
        {
            OnSoundPlayed?.Invoke(null, playerAudioData.jumpSFX);
        }

        public void PlayDashSFX()
        {
            OnSoundPlayed?.Invoke(null, playerAudioData.dashSFX);
        }

        public void PlayDeathSFX()
        {
            OnSoundPlayed?.Invoke(null, playerAudioData.deathSFX);
        }
    }
}