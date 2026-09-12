using TMPro;
using UnityEngine;

public class OrderScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private float displayTime = 2f;

    private float timer;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowScore(int score)
    {
        if (popupText == null)
        {
            return;
        }

        if (score >= 0)
        {
            popupText.text = "+" + score;
        }
        else
        {
            popupText.text = score.ToString();
        }

        timer = displayTime;

        gameObject.SetActive(true);
    }
}