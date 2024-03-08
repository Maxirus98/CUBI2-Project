using Unity.Netcode;
using UnityEngine;

public class EnemyStats : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 3;
    private int currentHealth;

    private WinLoseHandler winLoseHandler;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(1);
        }

        if (other.CompareTag("Bed"))
        {
            WinLoseHandler.Instance.UpdateGameState(GameState.Lost);
        }
    }

    private void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
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
        NetworkObject.Despawn(gameObject);
    }
}