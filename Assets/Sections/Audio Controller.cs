using System;
using UnityEngine;

namespace Sections
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private SectionData sectionData;
        [SerializeField] private AudioSource sfxSource;

        public static event Action<AudioClip, AudioSource> OnSoundPlay;

        public void PlayNewPersonSFX() { OnSoundPlay?.Invoke(sectionData.GetRandomPersonSpawnedSFX(), sfxSource); }
        public void PlayApprovePersonSFX() { OnSoundPlay?.Invoke(sectionData.approvePersonSFX, null); }
        public void PlayDenyPersonSFX() { OnSoundPlay?.Invoke(sectionData.denyPersonSFX, null); }
        public void PlaySectionsButtonsSFX() { OnSoundPlay?.Invoke(sectionData.sectionsButtonsSFX, null); }
        public void PlayRulesButtonSFX() { OnSoundPlay?.Invoke(sectionData.GetRandomRulesButtonSFX(), null); }
    }
}