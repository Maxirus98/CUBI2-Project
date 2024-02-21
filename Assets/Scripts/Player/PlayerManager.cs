using Unity.Netcode;
using UnityEngine;


/// <summary>
/// Script où tous les composants du joueur se rencontre pour intéragir entre eux
/// </summary>
public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private CameraHandler cameraHandler;
    private CameraHandler cameraHandlerClone;
    private InputHandler inputHandler;
    private PlayerController playerController;
    private PlayerStats playerStats;
    private PlayerCombat playerCombat;

    [Header("Player Flags")]
    public bool isInAir;
    public bool isGrounded;

    private void Awake()
    {
        if (!IsOwner) this.enabled = false;
    }

    private void Start()
    {
        InitializeComponents();
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
        inputHandler.TickInput();
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

    private void InitializeComponents()
    {
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();
        playerCombat = GetComponent<PlayerCombat>();

        inputHandler.enabled = true;
        playerController.enabled = true;
        playerStats.enabled = true;
        playerCombat.enabled = true;
    }
}
