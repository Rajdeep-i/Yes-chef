using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerItemHolder itemHolder =
            FindFirstObjectByType<PlayerItemHolder>();

        if (itemHolder == null)
        {
            return;
        }

        if (!itemHolder.IsHoldingItem)
        {
            return;
        }

        GameObject item =
            itemHolder.GetHeldItem();

        itemHolder.ClearHeldItem();

        Destroy(item);
    }
}