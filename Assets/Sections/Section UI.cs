using System;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Sections
{
    public class SectionUI : MonoBehaviour
    {
        [SerializeField] private SectionData sectionData;
        [SerializeField] private AudioController audioController;

        public static event Action<float> OnTransitionDurationChange;

        [Header("Canvas")]
        [SerializeField] private Canvas canvas;

        [Header("Sections")]
        [SerializeField] private List<SectionBehaviour> accessableSections = new();
        private SectionBehaviour currentSection;

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI rules1Text;
        [SerializeField] private TextMeshProUGUI rules2Text;
        [SerializeField] private TextMeshProUGUI lineCounterText;
        [SerializeField] private Animator uiAnimator;

        [Header("Error Indicators")]
        [SerializeField] private Image[] errorIndicators;
        [SerializeField] private Sprite activatedLight;

        private bool showingRules;

        private void Start()
        {
            //Atribui render Caemra do canvas
            canvas.worldCamera = CameraSystem.CameraManager.Instance.GetCamera();

            currentSection = accessableSections[0];

            currentSection.sectionCamera.Priority = int.MaxValue;

            string firstHalf = currentSection.sectionRulesFirstHalf;
            string secondHalf = currentSection.sectionRulesSecondHalf;

            rules1Text.text = firstHalf;
            rules2Text.text = secondHalf;
        }

        private void Update()
        {
            lineCounterText.text = currentSection.GetPeopleCount().ToString();
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

            string firstHalf = currentSection.sectionRulesFirstHalf;
            string secondHalf = currentSection.sectionRulesSecondHalf;

            rules1Text.text = firstHalf;
            rules2Text.text = secondHalf;
        }

        public void ToggleRules()
        {
            audioController.PlayRulesButtonSFX();

            if (!showingRules)
                uiAnimator.Play("Show Rules");
            else
                uiAnimator.Play("Hide Rules");

            showingRules = !showingRules;
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

        public void UpdateErrorIndicators(int currentLives)
        {
            int amount = Mathf.Clamp(3 - currentLives, 0, 3);

            for (int i = 0; i < amount; i++)
                errorIndicators[i].sprite = activatedLight;
        }
    }
}