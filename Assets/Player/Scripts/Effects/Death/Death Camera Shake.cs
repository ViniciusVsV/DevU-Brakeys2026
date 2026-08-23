using Unity.Cinemachine;
using UnityEngine;

namespace Player.Effects
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class DeathCameraShake : MonoBehaviour
    {
        [SerializeField] private PlayerEffectsData playerEffectsData;

        private CinemachineImpulseSource cinemachineImpulseSource;

        private void Awake()
        {
            cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void ApplyEffect()
        {
            cinemachineImpulseSource.GenerateImpulse(playerEffectsData.cameraShakeStrength);
        }
    }
}