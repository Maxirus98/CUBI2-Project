using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;

    private PlayerControls playerControls;
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    private Vector2 movementInput;

    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
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
    private void OnJump()
    {
        rb.AddForce(Vector3.up * 5f,ForceMode.Impulse);
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}
