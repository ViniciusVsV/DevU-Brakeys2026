using System;
using UnityEngine;

namespace ScoreScreenEffects
{
    public class StartEffects : MonoBehaviour
    {
        [SerializeField] private ScoreScreenData scoreScreenData;

        public static event Action<AudioClip> OnMusicPlay;
        public static event Action<bool> OnMusicFade;

        private void Start()
        {
            OnMusicPlay?.Invoke(scoreScreenData.scoreScreenMusic);
            OnMusicFade?.Invoke(false);
        }
    }
}