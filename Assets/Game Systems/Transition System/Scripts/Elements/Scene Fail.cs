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

        public void FailScene()
        {
            transitionData.transitionShaderMaterial.SetTexture("_Transition_Texture", transitionData.failTexture);

            transitionData.transitionShaderMaterial.DOFloat(-0.1f, "_Progress", transitionData.failDuration)
                .SetEase(transitionData.failEase)
                .SetDelay(transitionData.failStartDelay)
                .SetUpdate(true)
                .OnComplete(() =>
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    });
        }

        private void OnDisable()
        {
            transitionData.transitionShaderMaterial.DOKill();
        }
    }
}