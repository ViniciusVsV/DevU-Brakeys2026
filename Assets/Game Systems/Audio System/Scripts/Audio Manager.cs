using UnityEngine;
using UnityEngine.Audio;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioData audioData;

        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public void PlayMusic(AudioSource audioSource, AudioClip music)
        {
            AudioSource usedSource;
            usedSource = audioSource ? audioSource : musicSource;

            usedSource.outputAudioMixerGroup = musicSource.outputAudioMixerGroup;

            if (usedSource.clip != music)
            {
                usedSource.clip = music;
                usedSource.loop = true;

                usedSource.Play();
            }
        }
        public void StopMusic(AudioSource audioSource)
        {
            AudioSource usedSource;
            usedSource = audioSource ? audioSource : musicSource;

            usedSource.Stop();
        }

        public void PlayContinuousSFX(AudioSource audioSource, AudioClip sfx)
        {
            AudioSource usedSource;
            usedSource = audioSource ? audioSource : sfxSource;

            usedSource.outputAudioMixerGroup = sfxSource.outputAudioMixerGroup;

            usedSource.clip = sfx;
            usedSource.loop = true;

            usedSource.Play();
        }
        public void StopContinuousSFX(AudioSource audioSource)
        {
            AudioSource usedSource;
            usedSource = audioSource ? audioSource : sfxSource;

            usedSource.Stop();
        }

        public void PlaySFX(AudioSource audioSource, AudioClip sfx)
        {
            AudioSource usedSource;
            usedSource = audioSource ? audioSource : sfxSource;

            usedSource.outputAudioMixerGroup = sfxSource.outputAudioMixerGroup;

            usedSource.PlayOneShot(sfx);
        }
    }
}