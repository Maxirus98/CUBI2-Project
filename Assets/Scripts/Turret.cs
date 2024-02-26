using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject cannonPrefab;
    public bool isPlayerInRange = false;

    public List<GameObject> enemies = new List<GameObject>();
    private InputHandler inputHandler;
    private bool isBuilt = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            isPlayerInRange = true;
            if(inputHandler == null)
            {
                inputHandler = col.GetComponent<InputHandler>();
                inputHandler.nearbyTurret = this;
            }
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
            Vector3 buildPosition = transform.position + new Vector3(0, 4f, 0);
            GameObject canonInstance = Instantiate(cannonPrefab, buildPosition, Quaternion.Euler(0, 0, 90));
            var instanceNetworkObject = canonInstance.GetComponent<NetworkObject>();
            isBuilt = true;

            if (NetworkManager.Singleton.IsServer)
            {
                instanceNetworkObject.Spawn();
            }
        }
    }
}
