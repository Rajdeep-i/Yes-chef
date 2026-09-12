using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager =
            FindFirstObjectByType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogWarning(
                "ScoreManager not found."
            );

            return;
        }

        UpdateScoreUI();
    }

    private void Update()
    {
        if (scoreManager == null)
        {
            return;
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text =
            "Score: " +
            scoreManager.CurrentScore +
            "\nHigh Score: " +
            scoreManager.HighScore;
    }
}