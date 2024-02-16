using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float Horizontal;
    public float Vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    private PlayerControls inputActions;
    [SerializeField] private float moveSpeed;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    void Start()
    {
        inputActions = new PlayerControls();
    }

    public void TickInput(float delta)
    {
        MoveInput();
    }

    private void MoveInput()
    {
        Horizontal = movementInput.x;
        Vertical = movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
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

    private void OnToggleGameMenu()
    {
        GameManager.Instance.ToggleGameMenu();
    }
}
