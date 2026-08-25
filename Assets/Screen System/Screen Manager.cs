using System;
using Unity.Cinemachine;
using UnityEngine;

namespace ScreenSystem
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private ScreenData screenData;
        public static event Action<float> OnTransitionDurationChange;

        [SerializeField] private CinemachineCamera counterCamera;
        [SerializeField] private CinemachineCamera xRayCamera;
        [SerializeField] private CinemachineCamera dogCamera;

        private void Start()
        {
            OnTransitionDurationChange?.Invoke(screenData.transitionDuration);
        }

        public void FocusCounterCamera()
        {
            counterCamera.Priority = 100;
            xRayCamera.Priority = 0;
            dogCamera.Priority = 0;
        }

        public void FocusXRayCamera()
        {
            counterCamera.Priority = 0;
            xRayCamera.Priority = 100;
            dogCamera.Priority = 0;
        }

        public void FocusDogCamera()
        {
            counterCamera.Priority = 0;
            xRayCamera.Priority = 0;
            dogCamera.Priority = 100;
        }

        public void ChangeTransitionDuration()
        {
            OnTransitionDurationChange?.Invoke(screenData.transitionDuration);
        }
    }
}