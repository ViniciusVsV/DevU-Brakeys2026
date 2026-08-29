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
            Debug.Log("FADE IN!");
            musicSource.DOFade(0.4f, audioData.fadeInDuration).SetEase(audioData.fadeInEase)
                .SetUpdate(true);
        }

        public void ApplyFadeOut()
        {
            Debug.Log("FADE OUT!");
            musicSource.DOFade(0f, audioData.fadeOutDuration).SetEase(audioData.fadeOutEase)
                .SetUpdate(true);
        }
    }
}