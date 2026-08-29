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
            transitionScreen.gameObject.SetActive(true);
            transitionScreen.anchoredPosition = Vector2.zero;
        }

        public void ReEnterScene(Action onFinish)
        {
            Vector2 direction = transitionData.GetDirectionVector(transitionData.reEnterDirection);

            Vector2 newPosition = new Vector2(direction.x * 1920f, direction.y * 1080f);

            transitionScreen.DOAnchorPos(newPosition, transitionData.reRenterDuration)
                .SetEase(transitionData.reRenterEase)
                .SetUpdate(true)
                .SetDelay(transitionData.initialDelay)
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