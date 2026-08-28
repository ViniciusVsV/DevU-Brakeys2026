using System;
using DG.Tweening;
using UnityEngine;

namespace TransitionSystem
{
    //cÓDIGO para quando o jogador está entrando em uma cena
    public class SceneEnter : MonoBehaviour
    {
        [SerializeField] private TransitionData transitionData;
        [SerializeField] private RectTransform transitionScreen;

        private void Awake()
        {
            transitionScreen.position = Vector2.zero;
        }

        public void EnterScene(Action onFinish)
        {
            Vector2 newPosition = transitionData.GetDirectionVector(transitionData.enterDirection) * new Vector2(1920, 1080);

            transitionScreen.DOAnchorPos(newPosition, transitionData.enterDuration)
                .SetEase(transitionData.enterEase)
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