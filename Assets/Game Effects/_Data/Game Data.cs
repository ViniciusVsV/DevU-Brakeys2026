using DG.Tweening;
using UnityEngine;

namespace GameEffects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
    public class GameData : ScriptableObject
    {
        [Header("Start")]
        public AudioClip gameMusic;

        [Header("End")]
        public float timeSlowDuration;
        public Ease timeSlowEase;
        public float sectionFocusDuration;
        public AudioClip defeatSFX;
    }
}