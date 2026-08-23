using MenuSystem;
using MobileControls;
using UnityEngine;

public class PlayerMenuGlue : MonoBehaviour
{
    private PauseMenuController pauseMenuController;
    private PauseHandler pauseHandler;

    private void Start()
    {
        pauseMenuController = PauseMenuController.Instance;
        pauseHandler = FindAnyObjectByType<PauseHandler>();
    }

    private void OnEnable()
    {
        Player.InputHandler.OnPausePressed += TogglePause;
    }
    private void OnDisable()
    {
        Player.InputHandler.OnPausePressed -= TogglePause;
    }

    public void TogglePause(bool activate)
    {
        pauseMenuController.TogglePause(activate);
        pauseHandler.TogglePause(activate);
    }
}
