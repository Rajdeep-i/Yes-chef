using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] ingredientPrefabs;

    private int currentIngredientIndex = 0;

    public void Interact()
    {
        PlayerItemHolder itemHolder =
            FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            return;
        }

        if (itemHolder.IsHoldingItem)
        {
            return;
        }

        if (ingredientPrefabs == null ||
            ingredientPrefabs.Length == 0)
        {
            return;
        }

        // Get the next ingredient in sequence
        GameObject ingredient =
            Instantiate(
                ingredientPrefabs[currentIngredientIndex]
            );

        itemHolder.HoldItem(ingredient);

        Ingredient ingredientComponent =
            ingredient.GetComponent<Ingredient>();

        // Move to the next ingredient
        currentIngredientIndex++;

        if (currentIngredientIndex >= ingredientPrefabs.Length)
        {
            currentIngredientIndex = 0;
        }
    }
}