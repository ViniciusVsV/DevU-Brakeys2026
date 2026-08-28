using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TransitionSystem
{
    //Código para quando o jogador morrer ou falhar e a transição deve ser específica para isso
    //Deve ser chamado via glue code quando o evento de morte do jogador ocorrer
    //Pode ser igual a LevelExit caso não haja uma transição única para falhar
    public class SceneFail : MonoBehaviour
    {
        [SerializeField] private TransitionData transitionData;
        [SerializeField] private RectTransform transitionScreen;

        public void FailScene()
        {
            Vector2 newPosition = transitionData.GetDirectionVector(transitionData.failDirection) * new Vector2(1920, 1080);
            transitionScreen.position = newPosition;

            transitionScreen.DOAnchorPos(Vector2.zero, transitionData.failDuration)
                .SetEase(transitionData.failEase)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });
        }

        private void OnDisable()
        {
            transitionScreen.DOKill();
        }
    }
}