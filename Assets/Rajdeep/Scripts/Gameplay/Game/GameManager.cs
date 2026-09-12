using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameDuration = 180f;

    private float remainingTime;
    private bool gameRunning;

    public float RemainingTime => remainingTime;
    public bool GameRunning => gameRunning;

    private void Start()
    {
        StartGame();
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
        remainingTime = gameDuration;
        gameRunning = true;

        Debug.Log("Game Started! Time: 3:00");
    }

    private void EndGame()
    {
        gameRunning = false;

        Debug.Log("TIME UP! Game Over.");
    }
}