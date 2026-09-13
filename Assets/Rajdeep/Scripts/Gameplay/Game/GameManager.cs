using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameDuration = 180f;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text newHighScoreText;

    private float remainingTime;
    private bool gameRunning;

    private ScoreManager scoreManager;

    public float RemainingTime => remainingTime;
    public bool GameRunning => gameRunning;

    private void Start()
    {
        scoreManager =
            FindFirstObjectByType<ScoreManager>();

        gameRunning = false;
        remainingTime = gameDuration;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (newHighScoreText != null)
        {
            newHighScoreText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!gameRunning)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;

            EndGame();
        }
    }

    public void StartGame()
    {
        if (scoreManager == null)
        {
            scoreManager =
                FindFirstObjectByType<ScoreManager>();
        }

        remainingTime = gameDuration;
        gameRunning = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (newHighScoreText != null)
        {
            newHighScoreText.gameObject.SetActive(false);
        }
    }

    private void EndGame()
    {
        gameRunning = false;

        ShowGameOver();
    }

    private void ShowGameOver()
    {
        if (scoreManager == null)
        {
            scoreManager =
                FindFirstObjectByType<ScoreManager>();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (scoreManager == null)
        {
            return;
        }

        if (finalScoreText != null)
        {
            finalScoreText.text =
                "Final Score: " +
                scoreManager.CurrentScore;
        }

        if (highScoreText != null)
        {
            highScoreText.text =
                "High Score: " +
                scoreManager.HighScore;
        }

        if (newHighScoreText != null)
        {
            newHighScoreText.gameObject.SetActive(
                scoreManager.IsNewHighScore
            );
        }
    }
}