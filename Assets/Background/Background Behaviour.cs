using CameraSystem;
using UnityEngine;

namespace Background
{
    public class BackgroundBehaviour : MonoBehaviour
    {
        private Transform mainCamera;

        private void Start()
        {
            mainCamera = CameraManager.Instance.GetCameraTransform();

            transform.parent = mainCamera;
        }
    }
}