using DG.Tweening;
using UnityEngine;

namespace SectionSystem
{
    [CreateAssetMenu(fileName = "SectionData", menuName = "Scriptable Objects/SectionData")]
    public class SectionData : ScriptableObject
    {
        public float transitionDuration;

        [Header("Section's People")]
        public int maxWaitLength;
        public float personMoveDuration;
        public Ease personMoveEase;
    }
}