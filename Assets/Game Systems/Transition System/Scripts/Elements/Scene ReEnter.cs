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
        [SerializeField] private RectTransform transitionScreen;

        private void Awake()
        {
            transitionScreen.position = Vector2.zero;
        }

        public void ReEnterScene(Action onFinish)
        {
            Vector2 newPosition = transitionData.GetDirectionVector(transitionData.reEnterDirection) * new Vector2(1920, 1080);

            transitionScreen.DOAnchorPos(newPosition, transitionData.reRenterDuration)
                .SetEase(transitionData.reRenterEase)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    onFinish?.Invoke();
                });
        }

        private void OnDisable()
        {
            transitionScreen.DOKill();
        }
    }
}