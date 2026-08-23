using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerAudioData", menuName = "Scriptable Objects/Player/AudioData")]
    public class PlayerAudioData : ScriptableObject
    {
        public AudioClip[] walkSFXs;
        public AudioClip jumpSFX;
        public AudioClip dashSFX;
        public AudioClip deathSFX;
    }
}