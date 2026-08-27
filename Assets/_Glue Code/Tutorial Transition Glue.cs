using TransitionSystem;
using UnityEngine;

public class TutorialTransitionGlue : MonoBehaviour
{
    private TransitionManager transitionManager;

    private void Start()
    {
        transitionManager = TransitionManager.Instance;
    }

    private void OnEnable()
    {
        Tutorial.TutorialController.OnTutorialFinish += TransitionToGame;
    }
    private void OnDisable()
    {
        Tutorial.TutorialController.OnTutorialFinish -= TransitionToGame;
    }

    private void TransitionToGame(string gameSceneName)
    {
        transitionManager.ExitScene(gameSceneName);
    }
}