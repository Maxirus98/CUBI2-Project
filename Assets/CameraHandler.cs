using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler singleton;
    public Transform targetTransform;

    [SerializeField]
    private float lookSpeed = 0.1f;
    [SerializeField]
    private float followSpeed = 0.1f;
    [SerializeField]
    private float pivotSpeed = 0.03f;
    [SerializeField]
    private float minimumPivot = 0f;
    [SerializeField]
    private float maximumPivot = 35f;
    [SerializeField]
    private float minimumLookAngle = 0f;
    [SerializeField]
    private float maximumLookAngle = 90f;

    [SerializeField]
    public Transform cameraTransform;
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
            delta / followSpeed);
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
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;

        // Clamp pivotAngle entre le minimumPivot et le maximumPivot pour éviter d'aller sous ou plus haut que ces valeurs
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);
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

    private void OnDestroy()
    {
        targetTransform.GetComponent<PlayerManager>().cameraHandlerClone = null;
    }
}
