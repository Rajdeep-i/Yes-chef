using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController characterController;
    private PlayerInputActions inputActions;
    private GameManager gameManager;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions();
        gameManager = FindFirstObjectByType<GameManager>();
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
            return;
        }

        Vector2 input =
            inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 movement =
            new Vector3(input.x, 0f, input.y);

        characterController.Move(
            movement * moveSpeed * Time.deltaTime
        );
    }
}