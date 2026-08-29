using System;
using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace EndEffects
{
    public class DefeatEffects : MonoBehaviour
    {
        //Pausa o tempo do jogo seguindo um Ease
        //Foca na câmera da seção inicial (se não já etiver focada)
        //Fica um tempo olhando para a seção cheia
        //Chama a transição para o menu final
        //Desativa a UI
        //Invoca fade out na música
        [SerializeField] private EndData endData;
        [SerializeField] private string finalSceneName;

        private Coroutine coroutine;

        public static event Action<bool> OnGameStart;
        public static event Action<bool> OnDefeatEffectsStart;
        public static event Action<AudioClip, AudioSource> OnSoundPlay;
        public static event Action<string> OnDefeatEffectsFinished;

        private void Start()
        {
            OnGameStart?.Invoke(false);
        }

        public void ApplyEffects(CinemachineCamera camera)
        {
            if (coroutine != null)
                return;

            coroutine = StartCoroutine(EffectsRoutine(camera));
        }

        private IEnumerator EffectsRoutine(CinemachineCamera camera)
        {
            OnSoundPlay?.Invoke(endData.defeatSFX, null);
            OnDefeatEffectsStart?.Invoke(true);

            //Usa dotwwen para para o tempo seguindo uma duração e Ease
            yield return DOTween.To(
                () => Time.timeScale,
                x => Time.timeScale = x,
                0,
                endData.timeSlowDuration
            )
            .SetEase(endData.timeSlowEase)
            .SetUpdate(true)
            .WaitForCompletion();

            camera.Priority = 1000000;

            yield return new WaitForSecondsRealtime(endData.sectionFocusDuration);

            OnDefeatEffectsFinished?.Invoke(finalSceneName);
        }
    }
}