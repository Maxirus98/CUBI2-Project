using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public float MoveAmount;
    public float MouseX;
    public float MouseY;

    public bool JumpInput;

    private PlayerControls inputActions;
    [SerializeField] private float moveSpeed;
    private Vector2 movementInput;
    private Vector2 cameraInput;
    private PlayerController playerController;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.Enable();
        }
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput();
    }

    private void MoveInput()
    {
        Horizontal = movementInput.x;
        Vertical = movementInput.y;

        MoveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
        MouseX = cameraInput.x;
        MouseY = cameraInput.y;
    }

    // Les méthodes d'Input doivent être prefix par le mot "On" pour le PlayerInput component
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnMoveCamera(InputValue inputValue)
    {
        cameraInput = inputValue.Get<Vector2>();
    }

    private void OnJump()
    {
        JumpInput = true;
        playerController.HandleJump();
    }

    private void OnToggleGameMenu()
    {
        GameManager.Instance.ToggleGameMenu();
    }
}
