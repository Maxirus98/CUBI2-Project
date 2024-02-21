using Unity.Netcode;
using UnityEngine;

public class PlayerCombat : NetworkBehaviour
{
    [SerializeField] private Projectile sandmanProjectile;
    [SerializeField] private Projectile petProjectile;

    private Projectile currentProjectile;
    // [SerializeField] private AudioClip spawnClip;
    [SerializeField] private float projectileSpeed = 700f;

    private void Start()
    {
        currentProjectile  = transform.GetChild(0).gameObject.activeInHierarchy ? 
            sandmanProjectile : petProjectile;

        Debug.Log($"Current projectile chosen is: {currentProjectile} for player {gameObject.name}");
    }

    public void Attack()
    {
        var dir = transform.forward;

        // Envoi la requête pour exécuter l'attaque sur tous les clients à partir du même personnage.
        // On évite de spawner un objet sur le network, on fait simplement dire à l'autre joueur de tirer
        RequestFireServerRpc(dir);

        // On tire localement
        ExecuteShoot(dir);
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
        var projectile = Instantiate(currentProjectile, transform.position, Quaternion.identity);
        projectile.Init(dir * projectileSpeed);
        // AudioSource.PlayClipAtPoint(spawnClip, transform.position);
    }
}