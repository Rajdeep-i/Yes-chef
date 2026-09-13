using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseControlsPanel;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (pauseControlsPanel != null)
        {
            pauseControlsPanel.SetActive(false);
        }
    }

    public void OpenPauseMenu()
    {
        if (isPaused)
        {
            return;
        }

        isPaused = true;

        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        pauseControlsPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        if (!isPaused)
        {
            return;
        }

        isPaused = false;

        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        pauseControlsPanel.SetActive(false);
    }

    public void OpenPauseControls()
    {
        if (!isPaused)
        {
            return;
        }

        pausePanel.SetActive(false);
        pauseControlsPanel.SetActive(true);
    }

    public void ClosePauseControls()
    {
        if (!isPaused)
        {
            return;
        }

        pauseControlsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}