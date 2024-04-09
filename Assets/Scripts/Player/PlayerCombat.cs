using Unity.Netcode;
using UnityEngine;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField]
    private Projectile petProbjectile;
    [SerializeField]
    private Projectile sandmanProjectile;
    [SerializeField]
    private Transform petShootPoint;
    [SerializeField]
    private Transform sandmanShootPoint;

    private Projectile currentProjectile;
    private Transform currentShootPoint;
    

    [SerializeField] private float projectileSpeed = 1500f;
    public PlayerAnimatorHandler playerAnimatorHandler;

    private Camera currentCamera;
    private PlayerManager playerManager;
    

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        SwitchProjectileAndShootPoint();
    }

    private void Update()
    {
        InitializeCamera();
    }

    public void SwitchProjectileAndShootPoint()
    {
        playerManager.IsSandman = transform.GetChild(0).gameObject.activeInHierarchy;
        currentProjectile = playerManager.IsSandman ? sandmanProjectile : petProbjectile;
        currentShootPoint = playerManager.IsSandman ? sandmanShootPoint : petShootPoint;
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>(false);
    } 

    public void Attack()
    {
        var dir = transform.forward;
        // Envoi la requête pour exécuter l'attaque sur tous les clients à partir du même personnage.
        // On évite de spawner un objet sur le network, on fait simplement dire à l'autre joueur de tirer
        RequestFireServerRpc();

        // On tire localement
        Debug.Log("Fired locally");
        ExecuteShoot();

        // Anim
        playerAnimatorHandler.PlayTargetAnimationByName("Attack");
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestFireServerRpc()
    {
        Debug.Log("Fired Server Rpc");
        FireClientRpc();
    }

    [ClientRpc]
    private void FireClientRpc()
    {
        if (!IsOwner) {
            Debug.Log("Fired client rpc");
            ExecuteShoot();
        }
    }

    private void ExecuteShoot()
    {
        var dir = currentCamera.transform.forward;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
        var projectile = Instantiate(currentProjectile, currentShootPoint.position, Quaternion.identity);
        projectile.Init(dir * projectileSpeed);
        AudioSource.PlayClipAtPoint(playerManager.IsSandman ? SoundManager.Instance.sandmanAttackFx : SoundManager.Instance.gunShootFx, transform.position);
    }

    private void InitializeCamera()
    {
        if (currentShootPoint != null)
        {
            if (CameraHandler.singleton != null && currentCamera == null)
            {
                currentCamera = CameraHandler.singleton.cameraTransform.GetComponent<Camera>();
            }
        }
    }
}