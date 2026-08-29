using AudioSystem;
using UnityEngine;

public class AudioEverythingGlue : MonoBehaviour
{
    //Se inscreve nos eventos de todos os audioControllers de todos os elementos do jogo
    private void OnEnable()
    {
        Person.AudioController.OnSoundPlay += PlaySFX;
        Sections.AudioController.OnSoundPlay += PlaySFX;
        Minigames.AudioController.OnSoundPlay += PlaySFX;
        Minigames.AudioController.OnContinuousSFXPlay += PlayContinuousSFX;
        Minigames.AudioController.OnContinuousSFXStop += StopContinuousSFX;
        DialogueSystem.TextEffects.OnSoundPlay += PlaySFX;

        Tutorial.TutorialController.OnTutorialStart += FadeMusic;
        EndEffects.DefeatEffects.OnDefeatEffectsStart += FadeMusic;
        EndEffects.DefeatEffects.OnGameStart += FadeMusic;
        EndEffects.DefeatEffects.OnSoundPlay += PlaySFX;

        MenU.OnMenuMusicPlay += PlayMusic;
    }
    private void OnDisable()
    {
        Person.AudioController.OnSoundPlay -= PlaySFX;
        Sections.AudioController.OnSoundPlay -= PlaySFX;
        Minigames.AudioController.OnSoundPlay -= PlaySFX;
        Minigames.AudioController.OnContinuousSFXPlay -= PlayContinuousSFX;
        Minigames.AudioController.OnContinuousSFXStop -= StopContinuousSFX;
        DialogueSystem.TextEffects.OnSoundPlay -= PlaySFX;

        Tutorial.TutorialController.OnTutorialStart -= FadeMusic;
        EndEffects.DefeatEffects.OnDefeatEffectsStart -= FadeMusic;
        EndEffects.DefeatEffects.OnGameStart -= FadeMusic;
        EndEffects.DefeatEffects.OnSoundPlay -= PlaySFX;

        MenU.OnMenuMusicPlay -= PlayMusic;
    }

    public void PlayMusic(AudioClip music, AudioSource audioSource = null)
    {
        AudioManager.Instance.PlayMusic(audioSource, music);
    }

    public void PlayContinuousSFX(AudioClip sfx, AudioSource audioSource = null)
    {
        AudioManager.Instance.PlayContinuousSFX(audioSource, sfx); ;
    }

    public void StopContinuousSFX(AudioSource audioSource = null)
    {
        AudioManager.Instance.StopContinuousSFX(audioSource);
    }

    public void PlaySFX(AudioClip sfx, AudioSource audioSource = null)
    {
        AudioManager.Instance.PlaySFX(audioSource, sfx);
    }

    public void FadeMusic(bool fadingOut)
    {
        AudioEffects.Instance.ApplyMusicFade(fadingOut);
    }
}