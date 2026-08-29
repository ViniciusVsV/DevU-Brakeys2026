using System;
using GameEffects;
using Unity.Cinemachine;
using UnityEngine;

public class SectionGameGlue : MonoBehaviour
{
    [SerializeField] private DefeatEffects defeatEffects;

    private void OnEnable()
    {
        Sections.SectionBehaviour.OnGameDefeat += ApplyEndEffects;
    }
    private void OnDisable()
    {
        Sections.SectionBehaviour.OnGameDefeat -= ApplyEndEffects;
    }

    private void ApplyEndEffects(CinemachineCamera sectionCamera)
    {
        defeatEffects.ApplyLineFullEffects(sectionCamera);
    }
}