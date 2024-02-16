using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;

    private PlayerControls inputActions;
    [SerializeField] private float moveSpeed;
    private Vector2 movementInput;

    void Start()
    {
        inputActions = new PlayerControls();
    }

    void Update()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    // Les méthodes d'Input doivent être prefix par le mot "On" pour le PlayerInput component
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnToggleGameMenu()
    {
        GameManager.Instance.ToggleGameMenu();
    }
}
