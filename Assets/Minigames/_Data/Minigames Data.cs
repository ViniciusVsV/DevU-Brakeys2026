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
        public Ease passportHideEase;
        public float maxPassportRotation;
        public float minPassportMovement;
        public float maxPassportMovement;
        [Range(1f, 2f)] public float maxPassportSizeMultiplier;
    }
}