using System;
using UnityEngine;

public class MenU : MonoBehaviour
{
    [SerializeField] private GameObject PainelMainMenU;
    [SerializeField] private GameObject PainelOptions;
    [SerializeField] private AudioClip menuMusic;

    public static event Action<AudioClip, AudioSource> OnMenuMusicPlay;

    private void Start()
    {
        OnMenuMusicPlay?.Invoke(menuMusic, null);
    }

    public void Awake()
    {
        Time.timeScale = 1f;
    }

    public void LoadScenes(string Rscene)
    {
        TransitionSystem.TransitionManager.Instance.ExitScene(Rscene);
    }
    public void InOptions()
    {
        PainelMainMenU.SetActive(false);
        PainelOptions.SetActive(true);
    }
    public void OffOptions()
    {
        PainelMainMenU.SetActive(true);
        PainelOptions.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("bury the light deep with in");
        Application.Quit();
    }
}