using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey =
        "KitchenGame_HighScore";

    private int currentScore;
    private int highScore;

    public int CurrentScore => currentScore;
    public int HighScore => highScore;

    private void Awake()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        highScore =
            PlayerPrefs.GetInt(
                HighScoreKey,
                0
            );
    }

    public int GetIngredientValue(
        IngredientType ingredientType
    )
    {
        switch (ingredientType)
        {
            case IngredientType.Vegetable:
                return 20;

            case IngredientType.Cheese:
                return 10;

            case IngredientType.Meat:
                return 30;

            default:
                return 0;
        }
    }

    public int CalculateOrderScore(
        OrderData order,
        float elapsedTime
    )
    {
        if (order == null)
        {
            return 0;
        }

        int ingredientValueTotal = 0;

        foreach (
            IngredientType ingredient
            in order.requiredIngredients
        )
        {
            ingredientValueTotal +=
                GetIngredientValue(ingredient);
        }

        int timePenalty =
            Mathf.FloorToInt(elapsedTime);

        int orderScore =
            ingredientValueTotal -
            timePenalty;

        return orderScore;
    }

    public void AddOrderScore(int orderScore)
    {
        currentScore += orderScore;

        CheckForNewHighScore();

        Debug.Log(
            "Order Score: " +
            orderScore +
            " | Total Score: " +
            currentScore +
            " | High Score: " +
            highScore
        );
    }

    private void CheckForNewHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;

            PlayerPrefs.SetInt(
                HighScoreKey,
                highScore
            );

            PlayerPrefs.Save();

            Debug.Log(
                "NEW HIGH SCORE! " +
                highScore
            );
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;

        Debug.Log(
            "Current score reset."
        );
    }
}