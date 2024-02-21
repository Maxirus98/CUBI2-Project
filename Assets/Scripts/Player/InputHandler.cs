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

    // Combat Input
    private PlayerCombat playerCombat;
    private float lastFired = float.MinValue;
    private float attackCooldown = 0.5f;

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = true;

        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.Enable();
        }
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        // Ce composant est sur l'enfant, car le projectile change selon le personnage s�lectionn�
        playerCombat = GetComponentInChildren<PlayerCombat>();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput()
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

    // Les m�thodes d'Input doivent �tre prefix par le mot "On" pour le PlayerInput component
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

    private void OnAttack()
    {
        // Donne un cooldown aux attaques pour �viter de spawn le bouton d'attaque
        if(lastFired + attackCooldown < Time.time)
        {
            lastFired = Time.time;
            playerCombat.Attack();
        }
    }

    private void OnToggleGameMenu()
    {
        GameManager.Instance.ToggleGameMenu();
    }
}
