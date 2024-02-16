using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private Vector3 moveDirection;
    private Rigidbody rb;

    public Transform cameraObject;
    private InputHandler inputHandler;

    [Header("Movements Stats")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
    }

    #region Movement
    public void HandleMovement(float delta)
    {
        moveDirection = cameraObject.forward * inputHandler.Vertical;
        moveDirection += cameraObject.right * inputHandler.Horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var projectedVelocity = Vector3.ProjectOnPlane(moveDirection, hit.normal);
            rb.velocity = projectedVelocity * movementSpeed;
        }

        HandleRotation(delta);
    }

    private void HandleRotation(float delta)
    {
        Vector3 targetDirection;
        targetDirection = cameraObject.forward * inputHandler.Vertical;
        targetDirection += cameraObject.right * inputHandler.Horizontal;

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