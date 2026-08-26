using TransitionSystem;
using UnityEngine;

public class EndTransitionGlue : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = TransitionManager.Instance;
    }

    private void OnEnable()
    {
        EndEffects.DefeatEffects.OnDefeatEffectsFinished += TransitionToEndScreen;
    }
    private void OnDisable()
    {
        EndEffects.DefeatEffects.OnDefeatEffectsFinished -= TransitionToEndScreen;
    }

    private void TransitionToEndScreen(string finalSceneName)
    {
        transitionManager.ExitScene(finalSceneName);
    }
}