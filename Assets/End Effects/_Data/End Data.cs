using DG.Tweening;
using UnityEngine;

namespace EndEffects
{
    [CreateAssetMenu(fileName = "EndData", menuName = "Scriptable Objects/EndData")]
    public class EndData : ScriptableObject
    {
        public float timeSlowDuration;
        public Ease timeSlowEase;
        public float sectionFocusDuration;
        public AudioClip defeatSFX;
    }
}