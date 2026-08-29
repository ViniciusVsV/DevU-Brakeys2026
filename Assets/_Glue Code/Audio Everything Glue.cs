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
        GameEffects.DefeatEffects.OnSoundPlay += PlaySFX;

        Tutorial.TutorialController.OnMusicPlay += PlayMusic;
        Tutorial.TutorialController.OnMusicFade += FadeMusic;

        GameEffects.StartEffects.OnMusicPlay += PlayMusic;
        GameEffects.StartEffects.OnMusicFade += FadeMusic;
        GameEffects.DefeatEffects.OnMusicFade += FadeMusic;

        Scoring.OnSoundPlay += PlaySFX;

        ScoreScreenEffects.StartEffects.OnMusicPlay += PlayMusic;
        ScoreScreenEffects.StartEffects.OnMusicFade += FadeMusic;
    }
    private void OnDisable()
    {
        Person.AudioController.OnSoundPlay -= PlaySFX;
        Sections.AudioController.OnSoundPlay -= PlaySFX;
        Minigames.AudioController.OnSoundPlay -= PlaySFX;
        Minigames.AudioController.OnContinuousSFXPlay -= PlayContinuousSFX;
        Minigames.AudioController.OnContinuousSFXStop -= StopContinuousSFX;
        DialogueSystem.TextEffects.OnSoundPlay -= PlaySFX;
        GameEffects.DefeatEffects.OnSoundPlay -= PlaySFX;

        Tutorial.TutorialController.OnMusicPlay -= PlayMusic;
        Tutorial.TutorialController.OnMusicFade -= FadeMusic;

        GameEffects.StartEffects.OnMusicPlay -= PlayMusic;
        GameEffects.StartEffects.OnMusicFade -= FadeMusic;
        GameEffects.DefeatEffects.OnMusicFade -= FadeMusic;

        Scoring.OnSoundPlay -= PlaySFX;

        ScoreScreenEffects.StartEffects.OnMusicPlay -= PlayMusic;
        ScoreScreenEffects.StartEffects.OnMusicFade -= FadeMusic;
    }

    public void PlayMusic(AudioClip music)
    {
        AudioManager.Instance.PlayMusic(null, music);
    }

    public void FadeMusic(bool fadingOut)
    {
        AudioEffects.Instance.ApplyMusicFade(fadingOut);
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
}