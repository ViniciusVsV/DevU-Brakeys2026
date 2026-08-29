using GameEffects;
using Unity.Cinemachine;
using UnityEngine;

public class ScoreGameGlue : MonoBehaviour
{
    [SerializeField] private DefeatEffects defeatEffects;

    private void OnEnable()
    {
        Scoring.OnGameDefeat += StartDefeatEffects;
    }
    private void OnDisable()
    {
        Scoring.OnGameDefeat -= StartDefeatEffects;
    }

    private void StartDefeatEffects()
    {
        defeatEffects.ApplyThreeErrorsEffects();
    }
}