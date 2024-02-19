using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler singleton;
    public Transform targetTransform;
    
    private readonly float LOOK_SPEED = 0.1f;
    private readonly float FOLLOW_SPEED = 0.1f;
    private readonly float PIVOT_SPEED = 0.03f;
    private readonly float MINIMUM_PIVOT = -35f;
    private readonly float MAXIMUM_PIVOT = 35f;

    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform cameraPivotTransform;

    private Transform myTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private float lookAngle;
    private float pivotAngle;

    private void Awake()
    {
        singleton = this;
        myTransform = transform;
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void FollowTarget(float delta)
    {
        var targetPosition = Vector3.SmoothDamp(myTransform.position,
            targetTransform.position, ref cameraFollowVelocity,
            delta / FOLLOW_SPEED);
        myTransform.position = targetPosition;
    }

    /// <summary>
    /// Tourner la caméra selon la position de la souris
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="mouseXInput">Mouse position on X Axis</param>
    /// <param name="mouseYInput">Mouse position on Y Axis</param>
    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += (mouseXInput * LOOK_SPEED) / delta;
        pivotAngle -= (mouseYInput * PIVOT_SPEED) / delta;

        // Clamp pivotAngle entre le minimumPivot et le maximumPivot pour éviter d'aller sous ou plus haut que ces valeurs
        pivotAngle = Mathf.Clamp(pivotAngle, MINIMUM_PIVOT, MAXIMUM_PIVOT);

        // L'angle de vue du Camera Holder sur l'axe des y
        var rotation = Vector3.zero;
        rotation.y = lookAngle;
        var targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        // Camera Object Pivot Rotation
        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
}
