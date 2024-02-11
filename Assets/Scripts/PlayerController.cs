using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;
    private Rigidbody rb;

    private Transform cameraObject;
    private InputHandler inputHandler;

    [Header("Movements Stats")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
    }

    #region Movement
    private void Update()
    {
        var delta = Time.deltaTime;
        HandleMovement(delta);
    }

    private void HandleMovement(float delta)
    {
        moveDirection = transform.forward * inputHandler.vertical;
        moveDirection += transform.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var projectedVelocity = Vector3.ProjectOnPlane(moveDirection, hit.normal);
            rb.velocity = projectedVelocity * movementSpeed;
        }

        // HandleRotation(delta);
    }

    private void HandleRotation(float delta)
    {
        Vector3 targetDirection;
        targetDirection = cameraObject.forward * inputHandler.vertical;
        targetDirection += cameraObject.right * inputHandler.horizontal;

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