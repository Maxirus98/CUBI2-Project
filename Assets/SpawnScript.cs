using Unity.Netcode;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public bool isTesting = false;

    [SerializeField]
    public GameObject ennemy;

    [SerializeField]
    Transform[] spawnPoint;

    [SerializeField]
    public int NumberOfEnemies1;
    public int NumberOfEnemies2;

    void Start()
    {
        if (!isTesting && !NetworkManager.Singleton.IsHost) return;
        spawnPoint = new Transform[transform.childCount];

        for (int i=0; i<transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }

        for (int i = 0; i < NumberOfEnemies1; i++) {

            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemyInstance = Instantiate(ennemy, randomPoint.transform);
            var instanceNetworkObject = enemyInstance.GetComponent<NetworkObject>(); 
            instanceNetworkObject.Spawn();
        }
    }

    private void Update()
    {
        if(NumberOfEnemies1 <= 0)
        {
            WinLoseHandler.Instance.UpdateGameState(GameState.Won);
        }
    }
}
