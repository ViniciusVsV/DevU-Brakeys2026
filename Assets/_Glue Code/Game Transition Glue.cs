using TransitionSystem;
using UnityEngine;

public class GameTransitionGlue : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = TransitionManager.Instance;
    }

    private void OnEnable()
    {
        GameEffects.DefeatEffects.OnSceneExit += TransitionToEndScreen;
    }
    private void OnDisable()
    {
        GameEffects.DefeatEffects.OnSceneExit -= TransitionToEndScreen;
    }

    private void TransitionToEndScreen(string finalSceneName)
    {
        transitionManager.ExitScene(finalSceneName);
    }
}