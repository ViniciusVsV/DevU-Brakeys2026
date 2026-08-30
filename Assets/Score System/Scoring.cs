using UnityEngine;
using Person;
using Sections;
using Unity.Cinemachine;
using System;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private int lives = 3;

    public static event Action<AudioClip, AudioSource> OnSoundPlay;
    public static event Action<int> OnProcessMistake;
    public static event Action OnGameDefeat;

    private bool hasDied;

    [SerializeField] private AudioClip errorSFX;

    private void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Tutorial")
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("Joke");
        }
    }

    private void OnEnable()
    {
        SectionBehaviour.OnPersonProcessed += ProcessPerson;
    }
    private void OnDisable()
    {
        SectionBehaviour.OnPersonProcessed -= ProcessPerson;
    }

    public void ProcessPerson(PersonBehaviour person, bool isApproved)
    {
        if (hasDied)
            return;

        Debug.Log(person.name + " foi " + (isApproved ? "aprovado" : "reprovado") + " e tinha drogas? " + (person.isInvalid ? "Sim" : "Não"));

        if (person.isInvalid != isApproved)
        {
            score++;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetString("Joke", SetRankFromScore());

            PlayerPrefs.Save();

            Debug.Log("O jogador acertou! " + score.ToString());
        }
        else
        {
            lives--;

            OnProcessMistake?.Invoke(lives);

            if (lives <= 0)
            {
                OnGameDefeat?.Invoke();
                hasDied = true;
                return;
            }

            OnSoundPlay?.Invoke(errorSFX, null);
        }
    }

    public void ResetScore()
    {
        score = 0;
        lives = 3;
    }

    private string SetRankFromScore()
    {
        if (score >= 150)
            return "Supreme Airport Guardian";
        else if (score >= 140)
            return "International Threat Detector";
        else if (score >= 130)
            return "Master of Suspicious Eyebrows";
        else if (score >= 120)
            return "Passport Inspection Legend";
        else if (score >= 110)
            return "Enemy of Fake Passports";
        else if (score >= 100)
            return "Senior Security Wizard";
        else if (score >= 90)
            return "Professional Line Holder";
        else if (score >= 80)
            return "Certified Suspicion Expert";
        else if (score >= 70)
            return "Assistant to the Security Guard";
        else if (score >= 60)
            return "Security Guard";
        else if (score >= 50)
            return "Almost Trusted";
        else if (score >= 40)
            return "Badge Holder";
        else if (score >= 30)
            return "Temporary Airport Employee";
        else if (score >= 20)
            return "Dog's Assistant";
        else if (score >= 10)
            return "Airport Intern";
        else if (score >= 5)
            return "Intern";
        else if (score >= 2)
            return "Dog's Intern";
        else
            return "Unemployed";
    }
}