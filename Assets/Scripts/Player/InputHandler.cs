using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Turret nearbyTurret;

    public float Horizontal;
    public float Vertical;
    public float MoveAmount;

    public float MouseX;
    public float MouseY;

    private PlayerControls inputActions;

    [SerializeField] private float moveSpeed;
    private Vector2 movementInput;
    private Vector2 cameraInput;
    private PlayerController playerController;

    // Combat Input
    private PlayerCombat playerCombat;
    private float lastFired = float.MinValue;
    private float attackCooldown = 0.5f;

    private PlayerStats playerStats;
    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
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
        playerStats = GetComponent<PlayerStats>();
        // Ce composant est sur l'enfant, car le projectile change selon le personnage sélectionné
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        playerInput.enabled = false;
        playerController.enabled = false;
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

    // Les méthodes d'Input doivent être prefix par le mot "On" pour le PlayerInput component
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnMoveCamera(InputValue inputValue)
    {
        cameraInput = inputValue.Get<Vector2>();
    }

    private void OnAttack()
    {
        var attackCost = 1;
        // Donne un cooldown aux attaques pour éviter de spawn le bouton d'attaque
        if(lastFired + attackCooldown < Time.time && playerStats.CurrentUseable > 0)
        {
            lastFired = Time.time;
            playerCombat.Attack();
            playerStats.UseResource(attackCost);
        }
    }

    private void OnInteract()
    {
        playerStats.GainResource();
    }

    private void OnBuild()
    {
        var buildingCost = 5;
        if (nearbyTurret && playerStats.CurrentUseable >= buildingCost)
        {
            nearbyTurret.BuildCannon();
            playerStats.UseResource(buildingCost);
        }
    }

    private void OnToggleGameMenu()
    {
        GameManager.Instance.ToggleGameMenu();
    }
}
