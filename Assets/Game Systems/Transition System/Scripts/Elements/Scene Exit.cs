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
        [SerializeField] private RectTransform transitionScreen;

        public void ExitScene(string nextSceneName)
        {
            Vector2 direction = transitionData.GetDirectionVector(transitionData.exitDirection);

            Vector2 newPosition = new Vector2(direction.x * 1920f, direction.y * 1080f);
            transitionScreen.anchoredPosition = newPosition;

            transitionScreen.DOAnchorPos(Vector2.zero, transitionData.exitDuration)
                .SetEase(transitionData.exitEase)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(nextSceneName);
                });
        }

        private void OnDisable()
        {
            transitionScreen.DOKill();
        }
    }
}