using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    private HealthBar healthBarClone;

    [SerializeField]
    private int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        InitializeHealthbar();
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

    private void InitializeHealthbar()
    {
        healthBarClone = Instantiate(healthBar, Vector3.zero, Quaternion.identity);
        currentHealth = maxHealth;
        healthBarClone.SetMaxHealth(maxHealth);
    }

    private void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            healthBarClone.SetHealth(currentHealth);
        }
    }
}