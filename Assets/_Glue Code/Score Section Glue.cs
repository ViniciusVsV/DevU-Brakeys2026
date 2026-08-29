using System;
using Sections;
using Unity.Cinemachine;
using UnityEngine;

public class ScoreSectionGlue : MonoBehaviour
{
    [SerializeField] private SectionUI sectionUI;

    private void OnEnable()
    {
        Scoring.OnProcessMistake += UpdateUI;
    }
    private void OnDisable()
    {
        Scoring.OnProcessMistake -= UpdateUI;
    }

    private void UpdateUI(int currentLives)
    {
        sectionUI.UpdateErrorIndicators(currentLives);
    }
}