using Unity.Netcode;
using UnityEngine;

public class EnemyStats : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 3;
    private int currentHealth;

    private SpawnScript spawnScript;

    private void Start()
    {
        currentHealth = maxHealth;
        spawnScript = GameObject.Find("Spawner").GetComponent<SpawnScript>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Triggered Projectile");
            AudioSource.PlayClipAtPoint(SoundManager.Instance.enemyHitFx, transform.position);
            TakeDamage(1);
        }

        if (other.CompareTag("Door"))
        {
            Debug.Log("Triggered Door");
            WinLoseHandler.Instance.UpdateGameState(GameState.Lost);
        }
    }

    private void TakeDamage(int damage)
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
        if (spawnScript.NumberOfEnemies1 > 0)
        {
            spawnScript.NumberOfEnemies1 -= 1;
        }
    }
}