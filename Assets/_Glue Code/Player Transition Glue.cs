using TransitionSystem;
using UnityEngine;

public class PlayerTransitionGlue : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = TransitionManager.Instance;
    }

    private void OnEnable()
    {
        Player.DeathDetector.OnPlayerDeathCompleted += FailScene;
    }
    private void OnDisable()
    {
        Player.DeathDetector.OnPlayerDeathCompleted -= FailScene;
    }

    private void FailScene()
    {
        transitionManager.FailScene();
    }
}
