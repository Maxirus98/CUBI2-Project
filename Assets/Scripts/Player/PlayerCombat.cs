using Unity.Netcode;
using UnityEngine;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField]
    private Projectile currentProjectile;
    [SerializeField]
    private Transform shootPoint;
    // [SerializeField] private AudioClip spawnClip;
    [SerializeField] private float projectileSpeed = 1500f;

    [SerializeField] private PlayerAnimatorHandler playerAnimatorHandler;
    public void Attack()
    {
        var dir = transform.forward;

        // Envoi la requ�te pour ex�cuter l'attaque sur tous les clients � partir du m�me personnage.
        // On �vite de spawner un objet sur le network, on fait simplement dire � l'autre joueur de tirer
        RequestFireServerRpc(dir);

        // On tire localement
        ExecuteShoot(dir);


        // Anim
        playerAnimatorHandler.PlayTargetAnimationByName("Attack");
    }


    [ServerRpc]
    private void RequestFireServerRpc(Vector3 dir)
    {
        FireClientRpc(dir);
    }

    [ClientRpc]
    private void FireClientRpc(Vector3 dir)
    {
        if (!IsOwner) ExecuteShoot(dir);
    }

    private void ExecuteShoot(Vector3 dir)
    {
        var projectile = Instantiate(currentProjectile, shootPoint.position, Quaternion.identity);
        projectile.Init(dir * projectileSpeed);
        // AudioSource.PlayClipAtPoint(spawnClip, transform.position);
    }
}