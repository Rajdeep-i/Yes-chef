using UnityEngine;

public class PreparationTable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Player interacted with the Preparation Table!");
    }
}