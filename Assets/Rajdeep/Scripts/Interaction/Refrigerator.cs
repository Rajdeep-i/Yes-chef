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
            Debug.LogWarning("PlayerItemHolder not found.");
            return;
        }

        if (itemHolder.IsHoldingItem)
        {
            Debug.Log("Player is already holding an ingredient.");
            return;
        }

        if (ingredientPrefabs == null ||
            ingredientPrefabs.Length == 0)
        {
            Debug.LogWarning(
                "No ingredient prefabs assigned to the Refrigerator."
            );

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

        if (ingredientComponent != null)
        {
            Debug.Log(
                "Player got " +
                ingredientComponent.Type +
                " from the Refrigerator."
            );
        }

        // Move to the next ingredient
        currentIngredientIndex++;

        if (currentIngredientIndex >= ingredientPrefabs.Length)
        {
            currentIngredientIndex = 0;
        }
    }
}