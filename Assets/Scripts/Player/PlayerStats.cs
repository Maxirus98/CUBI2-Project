using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private PlayerResourceScript playerResource;
    private PlayerResourceScript playerResourceClone;

    [SerializeField]
    private int maxHealth = 5;
    private int currentHealth;

    private int maxUseable = 5;
    private int currentUseable;

    private float resourceDetectionRadius = 3f;

    void Start()
    {
        InitializePlayerResource();
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log($"Collided with {collision.gameObject.name}");
            TakeDamage(1);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"triggered with {other.gameObject.name}");
            TakeDamage(1);
        }
    }

    /// <summary>
    /// Action d'ajouter des ressources comme l'eau ou le sable lorsqu'on intéragit près de ces ressources.
    /// </summary>
    public void AddNearbyResource(string resourceTag)
    {
        var playerPosition = transform.position;
        if (Physics.SphereCast(playerPosition, resourceDetectionRadius, transform.position, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(resourceTag))
            {
                // TODO: Add resource to player pool
            }
        }
    }

    private void InitializePlayerResource()
    {
        playerResourceClone = Instantiate(playerResource, Vector3.zero, Quaternion.identity);
        currentHealth = maxHealth;
        playerResourceClone.SetMaxHealth(maxHealth);

        currentUseable = maxUseable;
        playerResourceClone.SetMaxUseable(maxUseable);
    }

    private void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            playerResourceClone.SetHealth(currentHealth);
        }
    }

    public void UseResource(int spending)
    {
        currentUseable -= spending;
        playerResourceClone.SetUseable(currentUseable);
    }

    private void GainResource(int gain)
    {
        if(currentUseable < maxUseable)
        {
            currentUseable += gain;
            playerResourceClone.SetUseable(currentUseable);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, resourceDetectionRadius);
    }
}