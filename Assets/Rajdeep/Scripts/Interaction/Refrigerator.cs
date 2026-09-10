using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Player interacted with the Refrigerator!");
    }
}