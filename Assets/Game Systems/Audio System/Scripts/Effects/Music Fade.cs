using DG.Tweening;
using UnityEngine;

namespace AudioSystem
{
    public class MusicFade : MonoBehaviour
    {
        [SerializeField] private AudioData audioData;
        [SerializeField] private AudioSource musicSource;

        public void ApplyFadeIn()
        {
            musicSource.DOFade(1f, audioData.fadeInDuration).SetEase(audioData.fadeInEase)
                .SetUpdate(true);
        }

        public void ApplyFadeOut()
        {
            musicSource.DOFade(0f, audioData.fadeOutDuration).SetEase(audioData.fadeOutEase)
                .SetUpdate(true);
        }
    }
}