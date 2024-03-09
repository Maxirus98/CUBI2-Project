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
            TakeDamage(1);
        }

        if (other.CompareTag("Bed"))
        {
            Debug.Log("Triggered bed");
            WinLoseHandler.Instance.UpdateGameState(GameState.Lost);
        }
    }

    private void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            // vérifier imédiatement si l'ennemi est mort
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
    /// Dit � tous les clients de r�duire le nombre d'ennemis
    /// </summary>
    [ClientRpc]
    private void SetEnemyCountClientRpc()
    {
        if (spawnScript.NumberOfEnemies > 0)
        {
            spawnScript.NumberOfEnemies -= 1;
        }
    }
}