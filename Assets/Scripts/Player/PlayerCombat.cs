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
    [SerializeField] private PlayerAnimatorHandler playerAnimatorHandler;

    private PlayerStats playerStats;
    private Transform pointer;
    private Camera currentCamera;
    private PlayerManager playerManager;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerManager = GetComponent<PlayerManager>();
        SwitchProjectileAndShootPoint();
    }

    private void Update()
    {
        SetUiPointerPositionToShootPoint();
    }

    public void SwitchProjectileAndShootPoint()
    {
        currentProjectile = playerManager.IsSandman ? sandmanProjectile : petProbjectile;
        currentShootPoint = playerManager.IsSandman ? sandmanShootPoint : petShootPoint;
    } 

    public void Attack()
    {
        var dir = transform.forward;

        // Envoi la requête pour exécuter l'attaque sur tous les clients à partir du même personnage.
        // On évite de spawner un objet sur le network, on fait simplement dire à l'autre joueur de tirer
        RequestFireServerRpc(dir);

        // On tire localement
        Debug.Log("Fired locally");
        ExecuteShoot(dir);

        // Anim
        playerAnimatorHandler.PlayTargetAnimationByName("Attack");
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestFireServerRpc(Vector3 dir)
    {
        Debug.Log("Fired Server Rpc");
        FireClientRpc(dir);
    }

    [ClientRpc]
    private void FireClientRpc(Vector3 dir)
    {
        if (!IsOwner) {
            Debug.Log("Fired client rpc");
            ExecuteShoot(dir);
        }
    }

    private void ExecuteShoot(Vector3 dir)
    {
        var projectile = Instantiate(currentProjectile, currentShootPoint.position, Quaternion.identity);
        projectile.Init(dir * projectileSpeed);
        AudioSource.PlayClipAtPoint(playerManager.IsSandman ? SoundManager.Instance.sandmanAttackFx : SoundManager.Instance.gunShootFx, transform.position);
    }

    /// <summary>
    /// SetUiPointerPositionToShootPoint vient vérifier que les éléments nécessaires a faire apparaitre le Pointeur pour tirer sont presents
    /// et il place le pointeur sur l'ecran a l'endroit ou le projectile sort.
    /// </summary>
    private void SetUiPointerPositionToShootPoint()
    {
        if (currentShootPoint != null)
        {
            if (CameraHandler.singleton != null && currentCamera == null)
            {
                currentCamera = CameraHandler.singleton.cameraTransform.GetComponent<Camera>();
            }

            if (currentCamera != null)
            {

                if (playerStats.PlayerResourceClone != null)
                {
                    if (pointer == null)
                    {
                        pointer = playerStats.PlayerResourceClone.transform.Find("Pointer");
                    }
                    else
                    {
                        var shootPointScreenPos = currentCamera.WorldToScreenPoint(currentShootPoint.position);
                        pointer.position = shootPointScreenPos;
                    }
                }
            }
        }
    }
}