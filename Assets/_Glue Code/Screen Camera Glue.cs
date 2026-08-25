using CameraSystem;
using UnityEngine;

public class ScreenCameraGlue : MonoBehaviour
{
    private CameraManager cameraManager;

    private void Start()
    {
        cameraManager = CameraManager.Instance;
    }

    private void OnEnable()
    {
        SectionSystem.SectionUI.OnTransitionDurationChange += ChangeTransitionDuration;
    }
    private void OnDisable()
    {
        SectionSystem.SectionUI.OnTransitionDurationChange -= ChangeTransitionDuration;
    }

    private void ChangeTransitionDuration(float newDuration)
    {
        cameraManager.SetBlendTime(newDuration);
    }
}
