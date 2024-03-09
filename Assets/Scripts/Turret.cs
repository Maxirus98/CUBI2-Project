using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Turret : NetworkBehaviour
{
    public GameObject cannonPrefab;
    private GameObject cannonInstance;
    public bool isPlayerInRange = false;
    public bool isBuilt = false;

    public float attackRate = 1f;
    private float attackCounter = 0f;

    public GameObject bulletPrefab;

    public List<GameObject> enemies = new List<GameObject>();

    private InputHandler inputHandler;
    [SerializeField]
    private GameObject buildText;

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
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            isPlayerInRange = true;
            buildText.SetActive(!isBuilt);
            inputHandler = col.GetComponent<InputHandler>();

            inputHandler.nearbyTurret = this;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            isPlayerInRange = false;
            buildText.SetActive(false);
            if (inputHandler != null)
            {
                inputHandler.nearbyTurret = null;
            }
        }
    }

    public void BuildCannon()
    {
        if (isPlayerInRange && cannonPrefab != null && !isBuilt)
        {
            BuildServerRpc();
        }
    }

    /// <summary>
    /// Le joueur/client dit au serveur de spawn un cannon sur la sc�ne 
    /// pour que tous les joueurs puissent le voir
    /// </summary>
    [ServerRpc(RequireOwnership = false)]
    private void BuildServerRpc()
    {
        if (IsServer)
        {
            // Spawn pour le joueur
            Vector3 buildPosition = transform.position + new Vector3(0, 4f, 0);
            cannonInstance = Instantiate(cannonPrefab, buildPosition, Quaternion.Euler(0, 0, 0));
            var instanceNetworkObject = cannonInstance.GetComponent<NetworkObject>();

            // Spawn pour le serveur
            instanceNetworkObject.Spawn();

            IsBuiltClientRpc();
        }
    }

    /// <summary>
    /// Dis � tous les joueurs/clients que cette tour est construite
    /// </summary>
    [ClientRpc]
    private void IsBuiltClientRpc()
    {
        isBuilt = true;
    }

    void FireAtEnemy()
    {
        GameObject targetEnemy = enemies[0];
        if (targetEnemy != null && cannonInstance != null)
        {
            Vector3 firePointOffset = transform.position + new Vector3(0, 4.5f, 1.5f);
            Vector3 firePoint = cannonInstance.transform.position + 
                                cannonInstance.transform.up * firePointOffset.y +
                                cannonInstance.transform.forward * firePointOffset.z;

            GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.identity);

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.SetTarget(targetEnemy.transform);

            // Assurez que la méthode Spawn() fonctionne bien sur le serveur
            NetworkObject bulletNetworkObject = bullet.GetComponent<NetworkObject>();
            if (bulletNetworkObject != null && IsServer)
            {
                bulletNetworkObject.Spawn();
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
}
