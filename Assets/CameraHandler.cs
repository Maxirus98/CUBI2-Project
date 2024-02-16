using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform cameraPivotTransform;

    private Transform myTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public static CameraHandler singleton;

    private float lookSpeed = 0.1f;
    private float followSpeed = 0.1f;
    private float pivotSpeed = 0.03f;

    private float targetPosition;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    private float minimumPivot = -35f;
    private float maximumPivot = 35f;

    // Camera Collision
    private float cameraSphereRadius = 0.2f;
    private float cameraCollisionOffset = 0.2f;
    private float minimumCollisionOffset = 0.2f;

    private void Awake()
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void FollowTarget(float delta)
    {
        /*Called everyframe, cause the camera to follow the player transform position
        SmoothDamp gradually changes a vector towards a desired goal over time
        The most common use is for smoothing a follow camera*/
        var targetPosition = Vector3.SmoothDamp(myTransform.position,
            targetTransform.position, ref cameraFollowVelocity,
            delta / followSpeed);
        myTransform.position = targetPosition;
    }

    /// <summary>
    /// Rotate the camera according to the position of the mouse
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="mouseXInput">Mouse position on X Axis</param>
    /// <param name="mouseYInput">Mouse position on Y Axis</param>
    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;

        // Clamp the pivotAngle between the minimumPivot and the maximumPivot so that pivotAngle can never go lower or higher than those values.
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        // Handle Camera Holder lookAngle on the y axis
        var rotation = Vector3.zero;
        rotation.y = lookAngle;
        var targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        // Hanlde pivotAngle on x axis
        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        // Handle Camera Pivot Rotation
        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
}
