using Unity.Netcode;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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
            Debug.Log("Triggered ProjectileSandMan");
            AudioSource.PlayClipAtPoint(SoundManager.Instance.enemyHitFx, transform.position);
            TakeDamage(1);
            anim.SetTrigger("Hit");
        }
        else if (other.CompareTag("ProjectilePet") && CompareTag("EnemyPet")) {
            Debug.Log("Triggered ProjectilePet");
            AudioSource.PlayClipAtPoint(SoundManager.Instance.enemyHitFx, transform.position);
            TakeDamage(1);
            anim.SetTrigger("Hit");
        }
        else if ((other.CompareTag("ProjectilePet") || other.CompareTag("ProjectileSandMan")) && CompareTag("Enemy")) {
            Debug.Log("Triggered ProjectilePet");
            AudioSource.PlayClipAtPoint(SoundManager.Instance.enemyHitFx, transform.position);
            TakeDamage(1);
            anim.SetTrigger("Hit");
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
        if (spawnScript.totalEnemies > 0)
        {
            spawnScript.totalEnemies -= 1;
        }
    }
}