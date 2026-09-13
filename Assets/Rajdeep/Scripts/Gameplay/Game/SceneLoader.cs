using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private string mainMenuSceneName;

    public void StartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            gameplaySceneName
        );
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            gameplaySceneName
        );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            mainMenuSceneName
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}