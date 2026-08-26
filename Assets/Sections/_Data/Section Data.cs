using UnityEngine;

namespace Sections
{
    [CreateAssetMenu(fileName = "SectionData", menuName = "Scriptable Objects/SectionData")]
    public class SectionData : ScriptableObject
    {
        public float transitionDuration;
        public int maxWaitLength;
        public float lineReorderDelay;
    }
}