using System;
using DG.Tweening;
using UnityEngine;

namespace TransitionSystem
{
    //Código para quando o jogador entra na mesma cena que estava antes, como se estivesse morrido e respawnado, por exemplo
    //Pode ser igual ao LevelEnter, caso o jogo não use esse tipo de comportamento
    public class SceneReEnter : MonoBehaviour
    {
        [SerializeField] private TransitionData transitionData;

        public void ReEnterScene(Action onFinish)
        {
            transitionData.transitionShaderMaterial.SetTexture("_Transition_Texture", transitionData.reRenterTexture);
            
            transitionData.transitionShaderMaterial
                .DOFloat(1f, "_Progress", transitionData.reRenterDuration)
                .SetEase(transitionData.reRenterEase)
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