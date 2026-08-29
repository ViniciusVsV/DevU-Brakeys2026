using UnityEngine;
using Person;
using Sections;
using Unity.Cinemachine;
using System;

public class Scoring : MonoBehaviour
{
    public static Scoring Instance;

    public int score = 0;
    public int lives = 3;
    public string joke = "Dog's Intern";

    public static event Action<AudioClip, AudioSource> OnSoundPlay;
    public static event Action<int> OnProcessMistake;
    public static event Action OnGameDefeat;

    private bool hasDied;

    [SerializeField] private AudioClip errorSFX;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public string GetRankFromScore(int score)
    {
        if (score >= 10)
            return "Safado";
        else if (score >= 7)
            return "Puliça";
        else if (score >= 5)
            return "Estagiário";
        else if (score >= 2)
            return "Cachorro Reserva";
        else
            return "Desempregado";
    }
}