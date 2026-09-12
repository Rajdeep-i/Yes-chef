using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRadius = 1.5f;
    [SerializeField] private GameObject interactionPrompt;

    private IInteractable currentInteractable;
    private PlayerInputActions inputActions;
    private GameManager gameManager;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        gameManager = FindFirstObjectByType<GameManager>();

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
        if (gameManager != null &&
            !gameManager.GameRunning)
        {
            currentInteractable = null;

            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }

            return;
        }

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
            interactionPrompt.SetActive(
                currentInteractable != null
            );
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