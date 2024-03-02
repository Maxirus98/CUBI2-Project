using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool IsNearResource { get; set; }

    [SerializeField]
    private PlayerResourceScript playerResource;

    [HideInInspector]
    public PlayerResourceScript PlayerResourceClone;

    [SerializeField]
    private int maxHealth = 5;
    private int currentHealth;

    private int maxUseable = 20;
    public int CurrentUseable;

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

    public void UseResource(int spending)
    {
        CurrentUseable -= spending;
        PlayerResourceClone.SetUseable(CurrentUseable);
    }

    public void GainResource()
    {
        // TODO: Dire plus d'information au joueur
        if (IsNearResource)
        {
            CurrentUseable = maxUseable;
            PlayerResourceClone.SetUseable(maxUseable);
        }
    }

    private void InitializePlayerResource()
    {
        PlayerResourceClone = Instantiate(playerResource, Vector3.zero, Quaternion.identity);
        currentHealth = maxHealth;
        PlayerResourceClone.SetMaxHealth(maxHealth);

        CurrentUseable = maxUseable / 2;
        PlayerResourceClone.SetMaxUseable(maxUseable);
    }

    private void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            PlayerResourceClone.SetHealth(currentHealth);
        }
    }
}