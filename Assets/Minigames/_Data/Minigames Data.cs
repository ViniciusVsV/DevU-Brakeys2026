using DG.Tweening;
using UnityEngine;

namespace Minigames
{
    [CreateAssetMenu(fileName = "MinigamesData", menuName = "Scriptable Objects/MinigamesData")]
    public class MinigamesData : ScriptableObject
    {
        [Header("Passport Viewing")]
        public float passportShowDuration;
        public Ease passportShowEase;
        public float passportHideDuration;
        public float maxPassportRotation;
        public float minPassportMovement;
        public float maxPassportMovement;
        [Range(1f, 2f)] public float maxPassportSizeMultiplier;

        [Header("Suitcase XRay")]
        public float minSuitcaseMoveDuration;
        public float maxSuitcaseMoveDuration;
        public Ease suitcaseMoveEase;
        public float suitcaseEndDuration;
        public Ease suitcaseEndEase;
        public float suitcaseDisappearDuration; 
        public Ease suitcaseDisappearEase;

        [Header("Dog Sniffing")]
        public float minSniffingDuration;
        public float maxSniffingDuration;
        [Range(0, 1)] public float repeatSniffingChance;
        public float minRepeatSniffingDelay;
        public float maxRepeatSniffingDelay;
        public float minRepeatSniffingDuration;
        public float maxRepeatSniffingDuration;
    }
}