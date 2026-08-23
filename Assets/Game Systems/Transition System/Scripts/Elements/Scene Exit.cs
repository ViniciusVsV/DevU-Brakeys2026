using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TransitionSystem
{
    //Código para quando o jogador sair de uma cena em geral
    public class SceneExit : MonoBehaviour
    {
        [SerializeField] private TransitionData transitionData;

        public void ExitScene(string nextSceneName)
        {
            transitionData.transitionShaderMaterial.SetTexture("_Transition_Texture", transitionData.exitTexture);

            transitionData.transitionShaderMaterial
                .DOFloat(-1f, "_Progress", transitionData.exitDuration)
                .SetEase(transitionData.exitEase)
                .OnComplete(() =>
                    {
                        SceneManager.LoadScene(nextSceneName);
                    });
        }

        private void OnDisable()
        {
            transitionData.transitionShaderMaterial.DOKill();
        }
    }
}