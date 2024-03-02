using Unity.Netcode;
using UnityEngine;


/// <summary>
/// Script o� tous les composants du joueur se rencontre pour int�ragir entre eux
/// </summary>
public class PlayerManager : NetworkBehaviour
{
    public bool testing;
    [SerializeField]
    private CameraHandler cameraHandler;
    private CameraHandler cameraHandlerClone;
    private InputHandler inputHandler;
    private PlayerController playerController;
    private PlayerStats playerStats;
    private Vector3 lastPosition;

    [Header("Player Flags")]
    public bool isInAir;
    public bool isGrounded;

    private void Awake()
    {
        if (!testing)
        {
            if (!IsOwner) this.enabled = false;
        }
    }

    private void Start()
    {
        InitializeComponents();
        // Spawn une cam�ra pour le joueur (Client)
        if (cameraHandlerClone == null)
        {
            cameraHandlerClone = Instantiate(cameraHandler);
            cameraHandlerClone.targetTransform = transform;
            playerController.CameraPrefab = cameraHandlerClone.transform;
        }
        lastPosition = transform.position;
    }

    void Update()
    {
        var delta = Time.deltaTime;
        inputHandler.TickInput();
        playerController.HandleMovement(delta);
        playerController.HandleFalling(delta);

        if (playerController.IsOnDune())
        {
            Debug.Log("On dune");
            transform.position = lastPosition;
        }
        else
        {
            lastPosition = transform.position;
        }
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

    private void InitializeComponents()
    {
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();

        inputHandler.enabled = true;
        playerController.enabled = true;
        playerStats.enabled = true;
    }
}
