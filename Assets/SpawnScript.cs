using Unity.Netcode;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public bool isTesting = false;
    [SerializeField]
    public GameObject ennemy;

    [SerializeField]
    Transform[] spawnPoint;

    public int NumberOfEnemies = 3;

    void Start()
    {
        if (!isTesting && !NetworkManager.Singleton.IsHost) return;
        spawnPoint = new Transform[transform.childCount];

        for (int i=0; i<transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }

        for (int i = 0; i < NumberOfEnemies; i++) {

            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemyInstance = Instantiate(ennemy, randomPoint.transform);
            var instanceNetworkObject = enemyInstance.GetComponent<NetworkObject>(); 
            instanceNetworkObject.Spawn();
        }
    }

    private void Update()
    {
        if(NumberOfEnemies <= 0)
        {
            WinLoseHandler.Instance.UpdateGameState(GameState.Won);
        }
    }
}
