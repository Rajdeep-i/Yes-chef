using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject ingredientPrefab;

    public void Interact()
    {
        PlayerItemHolder itemHolder = FindFirstObjectByType<PlayerItemHolder>();

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

        GameObject ingredient = Instantiate(ingredientPrefab);

        itemHolder.HoldItem(ingredient);

        Debug.Log("Player got an ingredient from the Refrigerator.");
    }
}