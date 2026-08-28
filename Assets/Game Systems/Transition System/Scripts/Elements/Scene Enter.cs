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
            transitionScreen.gameObject.SetActive(true);
            transitionScreen.anchoredPosition = Vector2.zero;
        }

        public void EnterScene(Action onFinish)
        {
            Vector2 direction = transitionData.GetDirectionVector(transitionData.enterDirection);

            Vector2 newPosition = new Vector2(direction.x * 1920f, direction.y * 1080f);

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