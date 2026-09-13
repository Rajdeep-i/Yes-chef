using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        GameManager gameManager =
            FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            return;
        }

        gameManager.StartGame();
    }
}