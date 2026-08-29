using DG.Tweening;
using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
    public class AudioData : ScriptableObject
    {
        [Header("Effects")]
        [Header("Music Fade")]
        public float fadeInDuration;
        public Ease fadeInEase;
        public float fadeOutDuration;
        public Ease fadeOutEase;
    }
}