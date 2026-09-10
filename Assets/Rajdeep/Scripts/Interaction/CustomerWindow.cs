using UnityEngine;

public class CustomerWindow : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Player interacted with the Customer Window!");
    }
}