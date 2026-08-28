using UnityEngine;
using Person;
using Sections;


public class Scoring : MonoBehaviour
{
    public int score = 0;
    public int lives = 3;
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
        //1 e 0 ou 0 e 1
        //se isApproved for true, a pessoa foi aprovada, se for false, a pessoa foi reprovada
        //O jogador acertou
        if (person.isInvalid != isApproved)
        {
            score++;
            Debug.Log("O jogador acertou!" + score.ToString());
            
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
        gameOverUI.ShowGameOver(score, joke);
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
