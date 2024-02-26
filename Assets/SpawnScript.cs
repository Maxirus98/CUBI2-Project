using Unity.Netcode;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public bool isTesting = false;
    [SerializeField]
    public GameObject ennemy;

    [SerializeField]
    Transform[] spawnPoint; 

    void Start()
    {
        if (!isTesting && !NetworkManager.Singleton.IsHost) return;
        spawnPoint = new Transform[transform.childCount];

        for (int i=0; i<transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }

        for (int i = 0; i < 3; i++) {

            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemyInstance = Instantiate(ennemy, randomPoint.transform);
            var instanceNetworkObject = enemyInstance.GetComponent<NetworkObject>(); 
            instanceNetworkObject.Spawn();
        }
    }
}
