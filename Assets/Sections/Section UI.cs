using System;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

namespace Sections
{
    public class SectionUI : MonoBehaviour
    {
        [SerializeField] private SectionData sectionData;
        public static event Action<float> OnTransitionDurationChange;

        [Header("Sections")]
        [SerializeField] private List<SectionBehaviour> accessableSections = new();
        private SectionBehaviour currentSection;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI rulesText;
        [SerializeField] private Animator uiAnimator;

        private void Start()
        {
            currentSection = accessableSections[0];

            currentSection.sectionCamera.Priority = int.MaxValue;
            rulesText.text = currentSection.sectionRules;
        }

        private void OnEnable()
        {
            SectionBehaviour.OnGameDefeat += DisableUI;
        }
        private void OnDisable()
        {
            SectionBehaviour.OnGameDefeat -= DisableUI;
        }

        public void FocusSection(int sectionIndex)
        {
            OnTransitionDurationChange?.Invoke(sectionData.transitionDuration);

            for (int i = 0; i < accessableSections.Count; i++)
            {
                if (i == sectionIndex)
                    accessableSections[i].sectionCamera.Priority = 100;
                else
                    accessableSections[i].sectionCamera.Priority = 0;
            }

            currentSection = accessableSections[sectionIndex];

            rulesText.text = currentSection.sectionRules;
        }

        public void ApproveActiveInCurrentSection()
        {
            currentSection.ApprovePerson();
        }

        public void DisapproveInCurrentSection()
        {
            currentSection.DisapprovePerson();
        }

        public void DisableUI(CinemachineCamera _)
        {
            uiAnimator.SetTrigger("Hide");
        }
    }
}