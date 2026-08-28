using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private HighscoreTable highscoreTable;

    private int finalScore;
    private string finalJoke;
    
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver(int score, string joke)
    {
        finalScore = score;
        finalJoke = joke;

        gameOverPanel.SetActive(true);

        nameInput.text = "";
        nameInput.ActivateInputField();
    }

    public void SaveScore()
    {
        string playerName = nameInput.text.ToUpper();

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

        gameOverPanel.SetActive(false);
    }
}