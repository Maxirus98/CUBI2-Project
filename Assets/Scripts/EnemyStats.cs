using Unity.Netcode;
using UnityEngine;

public class EnemyStats : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 3;
    private int currentHealth;

    private SpawnScript spawnScript;
    private Animator anim;

    private void Start()
    {
        currentHealth = maxHealth;
        spawnScript = GameObject.Find("Spawner").GetComponent<SpawnScript>();
        anim = GetComponentInChildren<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectileSandMan") && CompareTag("EnemySandMan"))
        {
            OnHit("Turret projectile Sandman");
        }
        else if (other.CompareTag("ProjectilePet") && CompareTag("EnemyPet")) {
            OnHit("Projectile Pet hit");

        }
        else if ((other.CompareTag("ProjectilePet") || other.CompareTag("ProjectileSandMan")) && CompareTag("Enemy")) {
            OnHit("Projectile player hit");
        }
        else if (other.CompareTag("Projectile") && (CompareTag("EnemySandMan") || CompareTag("EnemyPet") || CompareTag("Enemy")))
        {
            OnHit("Turret projectile Hit");
        }

        if (other.CompareTag("Door"))
        {
            WinLoseHandler.Instance.UpdateGameState(GameState.DoorLost);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            WinLoseHandler.Instance.UpdateGameState(GameState.DoorLost);
        }
    }

    private void OnHit(string message)
    {
        Debug.Log(message);
        AudioSource.PlayClipAtPoint(SoundManager.Instance.enemyHitFx, transform.position);
        TakeDamage(1);
        anim.SetTrigger("Hit");
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            // verifier immediatement si l'ennemi est mort
            if (currentHealth <= 0)
            {
                DestroyServerRpc();
            }
        } else
        {
            // TODO: Start particle effect or death animation before destroying
            DestroyServerRpc();
        }
    }

    /// <summary>
    /// Dit au serveur de despawn l'ennemi mort.
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    private void DestroyServerRpc()
    {
        SetEnemyCountClientRpc();
        NetworkObject.Despawn(gameObject);
    }

    /// <summary>
    /// Dit a tous les clients de reduire le nombre d'ennemis
    /// </summary>
    [ClientRpc]
    private void SetEnemyCountClientRpc()
    {
        if (spawnScript.totalEnemies > 0)
        {
            spawnScript.totalEnemies -= 1;
        }
    }
}