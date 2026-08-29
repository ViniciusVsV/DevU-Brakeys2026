using TransitionSystem;
using UnityEngine;

public class ScoreScreenTransitionGlue : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = TransitionManager.Instance;
    }

    private void OnEnable()
    {
        ScoreScreenEffects.ExitEffects.OnSceneExit += TransitionToNextScreen;
    }
    private void OnDisable()
    {
        ScoreScreenEffects.ExitEffects.OnSceneExit -= TransitionToNextScreen;
    }

    private void TransitionToNextScreen(string finalSceneName)
    {
        transitionManager.ExitScene(finalSceneName);
    }
}