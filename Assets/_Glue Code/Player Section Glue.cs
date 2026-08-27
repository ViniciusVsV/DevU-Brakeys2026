using Sections;
using UnityEngine;

public class PlayerSectionGlue : MonoBehaviour
{
    [SerializeField] private SectionUI sectionUI;

    private void OnEnable()
    {
        Player.InputHandler.OnSectionChosen += FocusSection;
    }
    private void OnDisable()
    {
        Player.InputHandler.OnSectionChosen -= FocusSection;
    }

    private void FocusSection(int sectionIndex)
    {
        sectionIndex = 3 - sectionIndex;
        sectionUI.FocusSection(sectionIndex);
    }
}