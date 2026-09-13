using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private Material preparedMaterial;
    [SerializeField] private Material cookedMaterial;

    private bool isPrepared = false;
    private bool isCooked = false;

    public IngredientType Type => ingredientType;
    public bool IsPrepared => isPrepared;
    public bool IsCooked => isCooked;

    public void Prepare()
    {
        // Only vegetables can be prepared
        if (ingredientType != IngredientType.Vegetable)
        {
            return;
        }

        isPrepared = true;

        Renderer ingredientRenderer =
            GetComponent<Renderer>();

        if (ingredientRenderer != null &&
            preparedMaterial != null)
        {
            ingredientRenderer.material =
                preparedMaterial;
        }
    }

    public void Cook()
    {
        // Only meat can be cooked
        if (ingredientType != IngredientType.Meat)
        {
            return;
        }

        isCooked = true;

        Renderer ingredientRenderer =
            GetComponent<Renderer>();

        if (ingredientRenderer != null &&
            cookedMaterial != null)
        {
            ingredientRenderer.material =
                cookedMaterial;
        }
    }
}