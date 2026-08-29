using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI jokeText;
    [SerializeField] private TextMeshProUGUI highscoreEntryConfirm;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button submitButton;

    [SerializeField] private HighscoreTable highscoreTable;

    private int finalScore;
    private string finalJoke;

    private void Start()
    {
        finalScore = Scoring.Instance.score;
        finalJoke = Scoring.Instance.joke;

        scoreText.text = "Score: " + finalScore.ToString();
        jokeText.text = "Your rank is: " + finalJoke;

        int amount = highscoreTable.GetHighscoreCount();
        if (amount < 10)
        {
            //Tem espaço para adicionar a pontuação 
            highscoreEntryConfirm.text = "You can enter the highscore table! Enter your name (3 letters):";
        }
        else
        {
            //Não tem espaço para adicionar a pontuação
            //Verifica se a pontuação do jogador é maior que a menor pontuação da tabela
            bool canEnter = highscoreTable.IsScoreHighEnough(finalScore);
            if (canEnter)
            {
                //A pontuação do jogador é maior que a menor pontuação da tabela, então ele pode entrar na tabela
                highscoreEntryConfirm.text = "You can enter the highscore table! Enter your name (3 letters):";
            }
            else
            {
                //A pontuação do jogador não é maior que a menor pontuação da tabela, então ele não pode entrar na tabela
                highscoreEntryConfirm.text = "You cannot enter the highscore table. Try again!";
                nameInput.interactable = false;
            }
        }
    }

    public void SaveScore()
    {
        string playerName = nameInput.text.ToUpper();

        submitButton.interactable = false;

        if (playerName.Length != 3)
        {
            Debug.Log("O nome precisa ter exatamente 3 letras!");
            return;
        }

        highscoreTable.AddHighscoreEntry(
            finalScore,
            playerName,
            finalJoke
        );
    }
}