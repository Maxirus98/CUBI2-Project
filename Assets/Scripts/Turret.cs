using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Turret : NetworkBehaviour
{
    public GameObject cannonPrefab;
    private GameObject cannonInstance;
    public GameObject towerPrefab;
    private GameObject towerInstance;
    public GameObject destroyTowerPrefab;
    private GameObject destroyTowerInstance;
    public bool isPlayerInRange = false;
    public bool isBuilt = false;

    public float attackRate = 1f;
    private float attackCounter = 0f;

    public GameObject bulletPrefab;

    public List<GameObject> enemies = new List<GameObject>();

    private InputHandler inputHandler;
    [SerializeField]
    private GameObject buildText;

    private Transform firePoint;

    // Construction des tours sur le reseau
    private NetworkList<ulong> netBuildingPlayerList = new(writePerm: NetworkVariableWritePermission.Owner);
    private Slider sandmanSyncSlider;
    private Slider petSyncSlider;
    private float animationTime = 0f;

    public Slider hpSlider;

    public int hp = 100;

    private int maxHp;


    void Start()
    {
        sandmanSyncSlider = GameObject.Find("InteractionUI").transform.Find("SandmanSyncSlider").GetComponent<Slider>();
        petSyncSlider = GameObject.Find("InteractionUI").transform.Find("PetSyncSlider").GetComponent<Slider>();

        firePoint = transform.Find("FirePoint");
        netBuildingPlayerList.OnListChanged += NetBuildingPlayerList_OnListChanged;

        maxHp = hp;
    }

    private void NetBuildingPlayerList_OnListChanged(NetworkListEvent<ulong> changeEvent)
    {
        // Lorsque les 2 joueurs ont fini de construire, construire la tour
        if (changeEvent.Type == NetworkListEvent<ulong>.EventType.Add)
        {
            Debug.Log($"Added to list, Size: {netBuildingPlayerList.Count}");
            if (netBuildingPlayerList.Count >= 2)
            {
                if(IsServer)
                {
                    BuildServerRpc();
                }
                AudioSource.PlayClipAtPoint(SoundManager.Instance.towerEndBuildFx, transform.position);
                HideSlidersClientRpc();
                buildText.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isBuilt && enemies.Count > 0)
        {
            attackCounter += Time.deltaTime;
            if (attackCounter >= (1f / attackRate))
            {
                attackCounter = 0f;
                FireAtEnemy();
            }
            RotateTowardsEnemy();
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("EnemyPet") || col.CompareTag("EnemySandMan"))
        {
            enemies.Add(col.gameObject);
        }
    }

    private float playerDetectionRange = 10f;
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            isPlayerInRange = (transform.position - col.transform.position).sqrMagnitude <= playerDetectionRange;
            var colNetworkObj = col.GetComponent<NetworkObject>();
            if (NetworkManager.LocalClientId == colNetworkObj.OwnerClientId)
            {
                var showUi = isPlayerInRange && !isBuilt;
                buildText.SetActive(showUi);
                sandmanSyncSlider.gameObject.SetActive(showUi);
                petSyncSlider.gameObject.SetActive(showUi);
                animationTime = isPlayerInRange ? animationTime : 0f;
                
                inputHandler = col.GetComponent<InputHandler>();
                if (inputHandler != null)
                {
                    inputHandler.nearbyTurret = isPlayerInRange ? this : null;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("EnemyPet") || col.CompareTag("EnemySandMan"))
        {
            enemies.Remove(col.gameObject);
        }

        if(col.CompareTag("Player"))
        {
            sandmanSyncSlider.value = 0f;
            petSyncSlider.value = 0f;
        }
    }

    public void BuildCannon()
    {
        if (isPlayerInRange && cannonPrefab != null && !isBuilt)
        {
            RequestToBuildServerRpc(NetworkManager.LocalClientId);
            AudioSource.PlayClipAtPoint(SoundManager.Instance.towerBuildingFx, transform.position);
        }
    }

    [ClientRpc]
    public void HideSlidersClientRpc()
    {
        sandmanSyncSlider.gameObject.SetActive(false);
        petSyncSlider.gameObject.SetActive(false);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestToBuildServerRpc(ulong clientId)
    {
        // TODO: Play request build audio

        // Fait apparaître le slider de synchronization pour les 2 joueurs
        var isSandman = NetworkManager.ConnectedClients[clientId].PlayerObject.transform.GetChild(0).gameObject.activeInHierarchy;
        AnimateSliderOverTimeClientRpc(clientId, isSandman);
    }

    [ClientRpc]
    private void AnimateSliderOverTimeClientRpc(ulong clientId, bool isSandman)
    {
        if(isPlayerInRange)
        {
            StartCoroutine(AnimateSliderOverTime(clientId, isSandman));
        }
    }

    private IEnumerator AnimateSliderOverTime(ulong clientId, bool isSandman)
    {
        var playerSlider = isSandman ? sandmanSyncSlider : petSyncSlider;
        playerSlider.gameObject.SetActive(true);

        animationTime = 0f;
        var seconds = 3f;

        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            playerSlider.value = Mathf.Lerp(0, seconds, lerpValue);
            yield return null;
        }

        // Apres avoir fini sa part de la construction, le joueur ajoute son id au reseau
        if (isPlayerInRange && !netBuildingPlayerList.Contains(clientId) && IsOwner)
        {
            netBuildingPlayerList.Add(clientId);
        }
    }

    /// <summary>
    /// Le joueur/client dit au serveur de spawn un cannon sur la sc�ne 
    /// pour que tous les joueurs puissent le voir
    /// </summary>
    [ServerRpc]
    public void BuildServerRpc()
    {
        if (IsServer)
        {
            // Spawn pour le joueur
            Vector3 buildTowerPosition = transform.position + Vector3.down;
            towerInstance = Instantiate(towerPrefab, buildTowerPosition, Quaternion.Euler(-90, 0, -30));
            var instanceTowerNetworkObject = towerInstance.GetComponent<NetworkObject>();

            Vector3 buildPosition = transform.position + new Vector3(0, 4f, 0);
            cannonInstance = Instantiate(cannonPrefab, buildPosition, Quaternion.Euler(0, 0, 0));
            var instanceCannonNetworkObject = cannonInstance.GetComponent<NetworkObject>();

            firePoint = cannonInstance.transform.Find("FirePoint");

            // Spawn pour le serveur
            instanceTowerNetworkObject.Spawn(true);
            instanceCannonNetworkObject.Spawn(true);
            IsBuiltClientRpc();
        }
    }

    /// <summary>
    /// Dis a tous les joueurs/clients que cette tour est construite
    /// </summary>
    [ClientRpc]
    private void IsBuiltClientRpc()
    {
        isBuilt = true;
    }

    void FireAtEnemy()
    {
        enemies.RemoveAll(item => item == null);
        GameObject targetEnemy = enemies[0];
        if (targetEnemy != null && cannonInstance != null)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Bullet bulletComponent = bulletInstance.GetComponent<Bullet>();
            bulletComponent.SetTarget(targetEnemy.transform);

            // Assurez que la méthode Spawn() fonctionne bien sur le serveur
            NetworkObject bulletNetworkObject = bulletInstance.GetComponent<NetworkObject>();
            AudioSource.PlayClipAtPoint(SoundManager.Instance.towerShootFx, transform.position);
            if (bulletNetworkObject != null && IsServer)
            {
                bulletNetworkObject.Spawn(true);
            }
            else
            {
                Debug.LogError("Trying to spawn bullet on non-server or NetworkObject is null");
            }
        }
    }

    void RotateTowardsEnemy()
    {
        if (enemies.Count > 0 && enemies[0] != null && cannonInstance != null)
        {
            GameObject targetEnemy = enemies[0];
            Vector3 targetDirection = targetEnemy.transform.position - transform.position;
            targetDirection.y = 0; // Gardez la rotation verticale à 0

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            cannonInstance.transform.rotation = Quaternion.Slerp(cannonInstance.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = (float)hp / maxHp;
        if (hp <= 0)
        {
            if (isBuilt)
            {
                isBuilt = false;
                Turret turretComponent = transform.GetComponent<Turret>();
                turretComponent.DestroyTowerServerRpc();
            }
        }
    }

    [ServerRpc]
    public void DestroyTowerServerRpc() {
        // Méthode pour détruire les tours sur le serveur
        Vector3 destroyPosition = transform.position + new Vector3(0, 0.5f, 0);
        destroyTowerInstance = Instantiate(destroyTowerPrefab, destroyPosition, Quaternion.Euler(0, 0, 0));
        var instanceNetworkObject = destroyTowerInstance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }
}
