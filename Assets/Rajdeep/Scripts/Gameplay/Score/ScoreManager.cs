using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey =
        "KitchenGame_HighScore";

    private int currentScore;
    private int highScore;

    private bool isNewHighScore;

    public int CurrentScore => currentScore;
    public int HighScore => highScore;

    public bool IsNewHighScore => isNewHighScore;

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
    }

    private void CheckForNewHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;

            isNewHighScore = true;

            PlayerPrefs.SetInt(
                HighScoreKey,
                highScore
            );

            PlayerPrefs.Save();
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;

        isNewHighScore = false;
    }
}