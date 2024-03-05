using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Turret : NetworkBehaviour
{
    public GameObject cannonPrefab;
    public bool isPlayerInRange = false;
    public bool isBuilt = false;

    public float attackRange = 10f;
    private float attackCounter = 0f;

    public GameObject bulletPrefab;

    public List<GameObject> enemies = new List<GameObject>();

    private InputHandler inputHandler;
    [SerializeField]
    private GameObject buildText;

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
            GameObject canonInstance = Instantiate(cannonPrefab, buildPosition, Quaternion.Euler(0, 0, 0));
            var instanceNetworkObject = canonInstance.GetComponent<NetworkObject>();

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

}
