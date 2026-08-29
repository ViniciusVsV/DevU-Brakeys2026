using UnityEngine;

namespace ScoreScreenEffects
{
    [CreateAssetMenu(fileName = "ScoreScreenData", menuName = "Scriptable Objects/ScoreScreenData")]
    public class ScoreScreenData : ScriptableObject
    {
        public AudioClip scoreScreenMusic;
    }
}