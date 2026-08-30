using UnityEngine;

namespace Sections
{
    [CreateAssetMenu(fileName = "SectionData", menuName = "Scriptable Objects/SectionData")]
    public class SectionData : ScriptableObject
    {
        public float transitionDuration;
        public int maxWaitLength;
        public float lineReorderDelay;

        [Header("Audio")]
        public AudioClip[] newPersonSpawnedSFXs;
        public AudioClip approvePersonSFX;
        public AudioClip denyPersonSFX;
        public AudioClip sectionsButtonsSFX;
        public AudioClip[] rulesButtonSFXs;

        public AudioClip GetRandomPersonSpawnedSFX() { return newPersonSpawnedSFXs[Random.Range(0, newPersonSpawnedSFXs.Length)]; }
        public AudioClip GetRandomRulesButtonSFX() { return rulesButtonSFXs[Random.Range(0, rulesButtonSFXs.Length)]; }
    }
}