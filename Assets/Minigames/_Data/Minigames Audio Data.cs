using UnityEngine;

namespace Minigames
{
    [CreateAssetMenu(fileName = "MinigamesAudioData", menuName = "Scriptable Objects/MinigamesAudioData")]
    public class MinigamesAudioData : ScriptableObject
    {
        [Header("Passport Viewing")]
        public AudioClip tvTurnOnSFX;
        public AudioClip tvTurnOffSFX;

        [Header("Suitcase X-Ray")]
        public AudioClip suitcaseFallSFX;
        public AudioClip beltSFX;

        [Header("Dog Sniffing")]
        public AudioClip sniffingSFX;
        public AudioClip finishedSniffingSFX;
    }
}