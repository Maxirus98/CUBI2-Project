using System.Resources;
using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    public bool IsNearResource { get; set; }
    public bool IsKo { get; set; }

    public int CurrentUseable;

    public PlayerResourceScript playerResource;

    [HideInInspector]
    public PlayerResourceScript PlayerResourceClone;

    [SerializeField]
    private int maxHealth = 5;
    [SerializeField]
    private int currentHealth;

    private int maxUseable = 20;
    private PlayerManager playerManager;
    private PlayerAnimatorHandler playerAnimatorHandler;

    public void Initialize()
    {
        InitializePlayerResource();
        playerManager = GetComponent<PlayerManager>();
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("EnemyPet") ||
            collision.collider.CompareTag("EnemySandMan") ||
            collision.collider.CompareTag("Cactus")) {
            Debug.Log($"Collided with {collision.gameObject.name}");
            TakeDamage(1);
            AudioSource.PlayClipAtPoint(SoundManager.Instance.attackEnemy1Fx, transform.position);
            if(playerAnimatorHandler != null)
            {
                playerAnimatorHandler.Hit();
            }
        }
    }

    public void ToggleResourceText(string text, bool isShowing)
    {
        if(PlayerResourceClone != null)
        {
            PlayerResourceClone.ToggleResourceText(text, isShowing);
        }
    }

    public void UseResource(int spending)
    {
        if(CurrentUseable > 0)
        {
            CurrentUseable -= spending;
            PlayerResourceClone.SetUseable(CurrentUseable);
        }
    }

    public void GainResource()
    {
        print("Gain Resource " + IsNearResource);
        if (IsNearResource)
        {
            CurrentUseable = maxUseable;
            PlayerResourceClone.SetUseable(maxUseable);
            currentHealth = maxHealth;
            PlayerResourceClone.SetHealth(currentHealth);
            AudioSource.PlayClipAtPoint(playerManager.IsSandman ? SoundManager.Instance.takeSandFx : SoundManager.Instance.takeWaterFx, transform.position);
        }
    }

    private void InitializePlayerResource()
    {
        PlayerResourceClone = Instantiate(playerResource, Vector3.zero, Quaternion.identity);
        currentHealth = maxHealth;
        PlayerResourceClone.SetMaxHealth(maxHealth);

        CurrentUseable = maxUseable;
        PlayerResourceClone.SetMaxUseable(maxUseable);
    }

    private void TakeDamage(int damage)
    {
        if (!IsOwner) return;
        if (currentHealth > 0)
        {
            Debug.Log($"Current health, {currentHealth} for player: {NetworkManager.Singleton.LocalClientId}");
            currentHealth -= damage;
            PlayerResourceClone.SetHealth(currentHealth);
        }

        if(currentHealth <= 0 && !IsKo)
        {
            SetKo();
        }
    }

    private void SetKo()
    {
        Debug.Log($"Set Ko called, {currentHealth}, for player: {NetworkManager.Singleton.LocalClientId}");
        IsKo = true;
        PlayerKo.Instance.UpdatePlayerKoListServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    private void OnDisable()
    {
        PlayerResourceClone = null;
    }
}