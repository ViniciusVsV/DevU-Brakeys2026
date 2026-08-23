using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MenuSystem
{
    public class BaseSection : MonoBehaviour
    {
        [SerializeField] private SectionEffectsData sectionEffectsData;
        [SerializeField] private AudioController audioController;

        [Header("Objects")]
        [SerializeField] protected RectTransform sectionPanel;
        [SerializeField] protected GameObject firstSelectedObject;
        protected GameObject previouslySelectedObject;

        public bool isActive;

        protected virtual void Awake()
        {
            sectionPanel.gameObject.SetActive(false);
        }

        //Animação padrão de ativação das Seções
        //Pode ser personalizada no override de uma seção especializada
        public virtual void Activate(GameObject previouslySelectedObject)
        {
            this.previouslySelectedObject = previouslySelectedObject;

            isActive = true;

            sectionPanel.localScale = Vector3.one * sectionEffectsData.finalSizeMultiplier;
            sectionPanel.gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(firstSelectedObject);

            if (audioController != null)
                audioController.PlaySFX(sectionEffectsData.openSFX);

            sectionPanel.DOScale(Vector3.one, sectionEffectsData.activationDuration)
                .SetEase(sectionEffectsData.activationEase)
                .SetUpdate(true);
        }

        //Animação padrão de desativação das Seções
        //Pode ser personalizada no override de uma seção especializada
        public virtual void Deactivate()
        {
            if (!isActive)
                return;

            isActive = false;

            if (audioController != null)
                audioController.PlaySFX(sectionEffectsData.closeSFX);

            sectionPanel.DOScale(Vector3.one * sectionEffectsData.finalSizeMultiplier, sectionEffectsData.deactivationDuration)
                .SetEase(sectionEffectsData.deactivationEase)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    sectionPanel.gameObject.SetActive(false);

                    if (previouslySelectedObject != null)
                        EventSystem.current.SetSelectedGameObject(previouslySelectedObject);
                });
        }

        private void OnDisable()
        {
            sectionPanel.DOKill();
        }
    }
}