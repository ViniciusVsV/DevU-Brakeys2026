using CameraSystem;
using UnityEngine;

public class SectionCameraGlue : MonoBehaviour
{
    private CameraManager cameraManager;

    private void Start()
    {
        cameraManager = CameraManager.Instance;
    }

    private void OnEnable()
    {
        Sections.SectionUI.OnTransitionDurationChange += ChangeTransitionDuration;
    }
    private void OnDisable()
    {
        Sections.SectionUI.OnTransitionDurationChange -= ChangeTransitionDuration;
    }

    private void ChangeTransitionDuration(float newDuration)
    {
        cameraManager.SetBlendTime(newDuration);
    }
}
