using Unity.Netcode;
using UnityEngine;


/// <summary>
/// Script ou tous les composants du joueur se rencontre pour interagir entre eux
/// </summary>
public class PlayerManager : NetworkBehaviour
{
    public bool testing;
    public CameraHandler cameraHandlerClone;

    [SerializeField]
    private CameraHandler cameraHandler;
    private InputHandler inputHandler;
    private PlayerController playerController;
    private PlayerStats playerStats;

    [Header("Player Flags")]
    public bool isInAir;
    public bool isGrounded;
    public bool IsSandman;

    public override void OnNetworkSpawn()
    {
        IsSandman = transform.GetChild(0).gameObject.activeInHierarchy;
    }

    private void Awake()
    {
        if (!testing)
        {
            if (!IsOwner) this.enabled = false;
            
        }

        if(testing)
        {
            IsSandman = transform.GetChild(0).gameObject.activeInHierarchy;
        }
    }

    private void OnEnable()
    {
        InitializeComponents();
    }

    void Update()
    {
        var delta = Time.deltaTime;
        inputHandler.TickInput();
        if (transform == null) return;
        playerController.HandleMovement(delta);
        playerController.HandleFalling(delta);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        if (cameraHandlerClone != null && transform != null)
        {
            cameraHandlerClone.FollowTarget(delta);
            cameraHandlerClone.HandleCameraRotation(delta, inputHandler.MouseX, inputHandler.MouseY);
        }
    }

    /// <summary>
    /// Spawn une camera pour le joueur (Client)
    /// </summary>
    public void InitializeCamera()
    {
        Debug.Log("Initialize Camera");
        if (cameraHandlerClone == null)
        {
            cameraHandlerClone = Instantiate(cameraHandler);
            cameraHandlerClone.targetTransform = transform;
            playerController.CameraPrefab = cameraHandlerClone.transform;
        }

        var playerSpawnPoint = GameObject.Find("PlayerSpawn");
        var offsetX = IsHost ? 4 : -4;
        transform.position = playerSpawnPoint.transform.position + Vector3.right * offsetX;
    }

    public void DeactivateComponents()
    {
        inputHandler.enabled = false;
        playerController.enabled = false;
        playerStats.enabled = false;

        cameraHandlerClone = null;
    }

    private void InitializeComponents()
    {
        Debug.Log("Initialize Components");
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
        playerStats = GetComponent<PlayerStats>();

        inputHandler.enabled = true;
        playerController.enabled = true;
        playerStats.enabled = true;

        playerStats.Initialize();
        InitializeCamera();
    }

    private void OnDisable()
    {
        DeactivateComponents();
    }
}
