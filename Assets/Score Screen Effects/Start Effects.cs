using System;
using UnityEngine;

namespace ScoreScreenEffects
{
    public class StartEffects : MonoBehaviour
    {
        [SerializeField] private ScoreScreenData scoreScreenData;

        [SerializeField] private Canvas canvas;

        public static event Action<AudioClip> OnMusicPlay;
        public static event Action<bool> OnMusicFade;

        private void Start()
        {
            canvas.worldCamera = CameraSystem.CameraManager.Instance.GetCamera();

            OnMusicPlay?.Invoke(scoreScreenData.scoreScreenMusic);
            OnMusicFade?.Invoke(false);
        }
    }
}