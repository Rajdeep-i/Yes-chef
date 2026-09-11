using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerItemHolder itemHolder = FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            Debug.LogWarning("PlayerItemHolder not found.");
            return;
        }

        if (!itemHolder.IsHoldingItem)
        {
            Debug.Log("Player is not holding anything to throw away.");
            return;
        }

        GameObject item = itemHolder.GetHeldItem();

        itemHolder.ClearHeldItem();

        Destroy(item);

        Debug.Log("Item thrown into the Trash Bin.");
    }
}