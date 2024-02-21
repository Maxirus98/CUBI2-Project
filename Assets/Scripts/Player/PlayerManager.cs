using Unity.Netcode;
using UnityEngine;


/// <summary>
/// Script où tous les composants du joueur se rencontre pour intéragir entre eux
/// </summary>
public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private CameraHandler cameraHandler;
    [SerializeField] 
    private CameraHandler cameraHandlerClone;
    private InputHandler inputHandler;
    private PlayerController playerController;

    [Header("Player Flags")]
    public bool isInAir;
    public bool isGrounded;

    private void Awake()
    {
        if (!IsOwner) this.enabled = false;
    }

    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        inputHandler.enabled = true;
        playerController = GetComponent<PlayerController>();
        playerController.enabled = true;

        // Spawn une caméra pour le joueur (Client)
        if (cameraHandlerClone == null)
        {
            cameraHandlerClone = Instantiate(cameraHandler);
            cameraHandlerClone.targetTransform = transform;
            playerController.CameraPrefab = cameraHandlerClone.transform;
        }
    }

    void Update()
    {
        var delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        playerController.HandleMovement(delta);
        playerController.HandleFalling(delta);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        if (cameraHandler != null)
        {
            cameraHandlerClone.FollowTarget(delta);
            cameraHandlerClone.HandleCameraRotation(delta, inputHandler.MouseX, inputHandler.MouseY);
        }
    }
}
