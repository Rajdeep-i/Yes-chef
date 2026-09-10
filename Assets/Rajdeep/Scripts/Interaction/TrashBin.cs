using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Player interacted with the Trash Bin!");
    }
}