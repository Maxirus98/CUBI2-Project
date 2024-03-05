using Unity.Netcode;
using UnityEngine;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField]
    private Projectile petProbjectile;
    [SerializeField]
    private Projectile sandmanProjectile;

    private Projectile currentProjectile;
    private Transform shootPoint;

    [SerializeField] private float projectileSpeed = 1500f;
    // [SerializeField] private AudioClip spawnClip;
    [SerializeField] private PlayerAnimatorHandler playerAnimatorHandler;

    public override void OnNetworkSpawn()
    {
        SwitchProjectile();
    }

    public void SwitchProjectile()
    {
        var isSandman = transform.GetChild(0).gameObject.activeInHierarchy;
        currentProjectile = isSandman ? sandmanProjectile : petProbjectile;
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

        var projectile = Instantiate(currentProjectile, transform.position, Quaternion.identity);
        projectile.Init(dir * projectileSpeed);
        // AudioSource.PlayClipAtPoint(spawnClip, transform.position);
    }
}