using System;
using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace GameEffects
{
    public class DefeatEffects : MonoBehaviour
    {
        //Pausa o tempo do jogo seguindo um Ease
        //Foca na câmera da seção inicial (se não já etiver focada)
        //Fica um tempo olhando para a seção cheia
        //Chama a transição para o menu final
        //Desativa a UI
        //Invoca fade out na música
        [SerializeField] private GameData gameData;
        [SerializeField] private string finalSceneName;

        private Coroutine coroutine;

        public static event Action<AudioClip, AudioSource> OnSoundPlay;
        public static event Action<bool> OnMusicFade;
        public static event Action<string> OnSceneExit;

        public void ApplyThreeErrorsEffects()
        {
            StartCoroutine(ThreeErrorsRoutine());
        }
        private IEnumerator ThreeErrorsRoutine()
        {
            //Dá slow no jogo e fade out na música, igual o outro, ams não foca na seção do passaporte
            OnSoundPlay?.Invoke(gameData.defeatSFX, null);
            OnMusicFade?.Invoke(true);

            //Usa dotwwen para para o tempo seguindo uma duração e Ease
            yield return DOTween.To(
                () => Time.timeScale,
                x => Time.timeScale = x,
                0,
                gameData.timeSlowDuration
            )
            .SetEase(gameData.timeSlowEase)
            .SetUpdate(true)
            .WaitForCompletion();

            yield return new WaitForSecondsRealtime(gameData.threeErrorsDelay);

            OnSceneExit?.Invoke(finalSceneName);
        }

        public void ApplyLineFullEffects(CinemachineCamera camera)
        {
            if (coroutine != null)
                return;

            coroutine = StartCoroutine(LineFullRoutine(camera));
        }
        private IEnumerator LineFullRoutine(CinemachineCamera camera)
        {
            OnSoundPlay?.Invoke(gameData.defeatSFX, null);
            OnMusicFade?.Invoke(true);

            //Usa dotwwen para para o tempo seguindo uma duração e Ease
            yield return DOTween.To(
                () => Time.timeScale,
                x => Time.timeScale = x,
                0,
                gameData.timeSlowDuration
            )
            .SetEase(gameData.timeSlowEase)
            .SetUpdate(true)
            .WaitForCompletion();

            camera.Priority = 1000000;

            yield return new WaitForSecondsRealtime(gameData.sectionFocusDuration);

            OnSceneExit?.Invoke(finalSceneName);
        }
    }
}