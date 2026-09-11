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
        isPrepared = true;

        Renderer ingredientRenderer = GetComponent<Renderer>();

        if (ingredientRenderer != null && preparedMaterial != null)
        {
            ingredientRenderer.material = preparedMaterial;
        }

        Debug.Log(ingredientType + " has been prepared!");
    }

    public void Cook()
    {
        if (!isPrepared)
        {
            Debug.LogWarning("Ingredient must be prepared before cooking.");
            return;
        }

        isCooked = true;

        Renderer ingredientRenderer = GetComponent<Renderer>();

        if (ingredientRenderer != null && cookedMaterial != null)
        {
            ingredientRenderer.material = cookedMaterial;
        }

        Debug.Log(ingredientType + " has been cooked!");
    }
}