using System;
using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace GameEffects
{
    public class StartEffects : MonoBehaviour
    {
        [SerializeField] private GameData gameData;

        public static event Action<AudioClip> OnMusicPlay;
        public static event Action<bool> OnMusicFade;

        private void Start()
        {
            OnMusicPlay?.Invoke(gameData.gameMusic);
            OnMusicFade?.Invoke(false);
        }
    }
}