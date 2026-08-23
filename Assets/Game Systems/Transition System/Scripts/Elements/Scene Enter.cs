using System;
using DG.Tweening;
using UnityEngine;

namespace TransitionSystem
{
    //cÓDIGO para quando o jogador está entrando em uma cena
    public class SceneEnter : MonoBehaviour
    {
        [SerializeField] private TransitionData transitionData;

        private void Awake()
        {
            transitionData.transitionShaderMaterial.SetFloat("_Progress", -1);
        }

        public void EnterScene(Action onFinish)
        {
            transitionData.transitionShaderMaterial.SetTexture("_Transition_Texture", transitionData.enterTexture);

            transitionData.transitionShaderMaterial
                .DOFloat(1f, "_Progress", transitionData.enterDuration)
                .SetEase(transitionData.enterEase)
                .OnComplete(() =>
                    {
                        onFinish?.Invoke();
                    });
        }

        private void OnDisable()
        {
            transitionData.transitionShaderMaterial.DOKill();
        }
    }
}