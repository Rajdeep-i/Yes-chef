using UnityEngine;

public class PlayerItemHolder : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;

    private GameObject heldItem;

    public bool IsHoldingItem => heldItem != null;

    public void HoldItem(GameObject item)
    {
        if (heldItem != null)
        {
            return;
        }

        heldItem = item;

        heldItem.transform.SetParent(holdPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        Collider itemCollider = heldItem.GetComponent<Collider>();

        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }
    }

    public GameObject GetHeldItem()
    {
        return heldItem;
    }

    public void ClearHeldItem()
    {
        heldItem = null;
    }

    public void DropItem()
    {
        if (heldItem == null)
        {
            return;
        }

        heldItem.transform.SetParent(null);

        Collider itemCollider = heldItem.GetComponent<Collider>();

        if (itemCollider != null)
        {
            itemCollider.enabled = true;
        }

        heldItem = null;
    }
}