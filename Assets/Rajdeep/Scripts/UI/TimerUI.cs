using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager =
            FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogWarning("GameManager not found.");
            return;
        }

        UpdateTimerUI();
    }

    private void Update()
    {
        if (gameManager == null)
        {
            return;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        float remainingTime =
            gameManager.RemainingTime;

        int minutes =
            Mathf.FloorToInt(remainingTime / 60f);

        int seconds =
            Mathf.FloorToInt(remainingTime % 60f);

        timerText.text =
            "Time: " +
            minutes.ToString("00") +
            ":" +
            seconds.ToString("00");
    }
}