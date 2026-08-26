using System;
using EndEffects;
using Unity.Cinemachine;
using UnityEngine;

public class SectionEndGlue : MonoBehaviour
{
    [SerializeField] private DefeatEffects endEffects;

    private void OnEnable()
    {
        GameSections.SectionBehaviour.OnGameDefeat += ApplyEndEffects;
    }
    private void OnDisable()
    {
        GameSections.SectionBehaviour.OnGameDefeat -= ApplyEndEffects;
    }

    private void ApplyEndEffects(CinemachineCamera sectionCamera)
    {
        endEffects.ApplyEffects(sectionCamera);
    }
}