using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Player interacted with the Stove!");
    }
}