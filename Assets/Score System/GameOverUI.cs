using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI jokeText;

    private int finalScore;
    private string finalJoke;

    private void Start()
    {
        Time.timeScale = 1f;

        finalScore = PlayerPrefs.GetInt("Score", 0);
        finalJoke = PlayerPrefs.GetString("Joke", "");

        scoreText.text = "Score: " + finalScore.ToString();
        jokeText.text = "Your rank is: " + finalJoke;
    }
}