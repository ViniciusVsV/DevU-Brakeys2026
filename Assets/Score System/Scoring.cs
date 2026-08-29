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
    public static event Action<CinemachineCamera> OnGameDefeat;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    // [SerializeField] private GameOverUI gameOverUI;

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
        SectionBehaviour.OnPersonProcessed += ProcessPersonApproved;
    }

    private void OnDisable()
    {
        SectionBehaviour.OnPersonProcessed -= ProcessPersonApproved;
    }

    public void ProcessPersonApproved(PersonBehaviour person, bool isApproved)
    {
        Debug.Log(person.name + " foi " + (isApproved ? "aprovado" : "reprovado") + " e tinha drogas? " + (person.isInvalid ? "Sim" : "Não"));

        //1 e 0 ou 0 e 1
        //se isApproved for true, a pessoa foi aprovada, se for false, a pessoa foi reprovada
        //O jogador acertou
        if (person.isInvalid != isApproved)
        {
            score++;
            Debug.Log("O jogador acertou! " + score.ToString());
        }
        else
        {
            // 1 e 1 ou 0 e 0
            //A pessoa foi aprovada e tinha drogas, ou a pessoa não foi aprovada e não tinha drogas
            //O jogador errou
            lives--;
            if (lives <= 0)
            {
                Debug.Log("Game Over!");
                ProcessGameOver();

            }
            else
            {
                Debug.Log("O jogador errou!" + lives.ToString());
            }
        }
    }

    public void ProcessGameOver()
    {
        Debug.Log("Game Over! Sua pontuação final foi: " + score.ToString());
        // gameOverUI.ShowGameOver(score, joke);
        OnGameDefeat?.Invoke(cinemachineCamera);
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
