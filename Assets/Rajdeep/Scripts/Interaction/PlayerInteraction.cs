using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1.5f;
    [SerializeField] private GameObject interactionPrompt;

    private IInteractable currentInteractable;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        DetectInteractable();

        if (currentInteractable != null &&
            inputActions.Player.Interact.WasPressedThisFrame())
        {
            currentInteractable.Interact();
        }
    }

    private void DetectInteractable()
    {
        currentInteractable = null;

        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            interactionRadius
        );

        foreach (Collider collider in colliders)
        {
            IInteractable interactable =
                collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                break;
            }
        }

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(currentInteractable != null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            interactionRadius
        );
    }
}