using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public Transform CameraPrefab;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private InputHandler inputHandler;
    private PlayerManager playerManager;
    private PlayerAnimatorHandler playerAnimatorHandler;

    [Header("Movements Stats")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float fallingSpeed = 90f;
    [SerializeField] private float jumpingForce = 15f;

    [Header("Ground & Air Detection Stats")]
    [SerializeField] private float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField] private float groundDetectionRayStartPoint = 0.5f;
    private float groundDirectionRayDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        playerManager = GetComponent<PlayerManager>();
        groundDirectionRayDistance = groundDetectionRayStartPoint + 0.07f;
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
    }

    #region Movement
    private Vector3 normalVector;

    public void HandleMovement(float delta)
    {
        moveDirection = CameraPrefab.forward * inputHandler.Vertical;
        moveDirection += CameraPrefab.right * inputHandler.Horizontal;
        moveDirection.Normalize();

        RaycastHit hit;
        // TODO: Add LayerMask to players and ignore that layer mask so they don't just walk on eachother's head
        var originRay = transform.position + Vector3.down * groundDetectionRayStartPoint;
        Debug.DrawRay(originRay, Vector3.down * groundDirectionRayDistance, Color.red);
        playerManager.isGrounded = Physics.Raycast(originRay, Vector3.down, out hit, groundDirectionRayDistance);
        if (playerManager.isGrounded)
        {
            normalVector = hit.normal;
            var projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rb.velocity = projectedVelocity * movementSpeed;
        }

        HandleRotation(delta);

        playerAnimatorHandler.UpdateAnimatorValues(inputHandler.MoveAmount);
    }

    public bool IsOnDune()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, LayerMask.GetMask("DuneNonWalkable")))
        {
            return true;
        }
        return false;
    }

    public void HandleFalling(float delta)
    {
        // Si tombe en bas de la plateforme par hasard, le ramener au sol
        var myPos = transform.position;
        if (myPos.y < -5)
        {
            // TODO: la position va changer selon la normale de la surface
            // Ou ramener au starting position
            myPos.y = 2f;
            transform.position = myPos;
        }

        // TODO: Faire jouer l'animation Falling
        var originRay = transform.position + Vector3.down * groundDetectionRayStartPoint;
        playerManager.isGrounded = Physics.Raycast(originRay, Vector3.down, out RaycastHit hit, groundDirectionRayDistance);
        if (!playerManager.isGrounded)
        {
            rb.AddForce(delta * fallingSpeed * Vector3.down, ForceMode.VelocityChange);
            // Pouce le joueur pour pas qu'il soit stuck sur les coins
            rb.AddForce(moveDirection * 2);
        }
    }

    private void HandleRotation(float delta)
    {
        Vector3 targetDirection;
        targetDirection = CameraPrefab.forward * inputHandler.Vertical;
        targetDirection += CameraPrefab.right * inputHandler.Horizontal;

        targetDirection.Normalize();
        targetDirection.y = 0f;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        var rs = rotationSpeed;
        var tr = Quaternion.LookRotation(targetDirection);
        var targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * delta);
        transform.rotation = targetRotation;
    }
    #endregion
}