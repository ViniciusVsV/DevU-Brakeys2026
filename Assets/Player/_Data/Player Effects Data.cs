using DG.Tweening;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerEffectsData", menuName = "Scriptable Objects/Player/EffectsData")]
    public class PlayerEffectsData : ScriptableObject
    {
        [Header("DEATH")]
        [Header("Sprite Flash")]
        public float flashDuration;
        public Color flashColor;
        public Ease flashEase;

        [Header("Camera Shake")]
        public float cameraShakeStrength;

        [Header("Knockback")]
        public float deathKnockbackDistance;
        public float deathKnockbackDuration;
        public Ease deathKnockbackEase;
        public float wallDetectedOffset;

        [Header("Time Slow")]
        public float slowDuration;

        [Header("Controller Rumble")]
        public float deathLowFrequency;
        public float deathHighFrequency;
        public float deathRumbleDuration;

        [Header("DASH")]
        [Header("After Images")]
        public float imageDuration;
        public float startingAlpha;
        public float delayBetweenImages;

        [Header("MOVEMENT")]
        public float runParticlesCooldown;
    }
}