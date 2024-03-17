using Unity.Netcode;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public bool isTesting = false;

    [SerializeField]
    public GameObject enemy1;
    public GameObject enemy2;

    [SerializeField]
    Transform[] spawnPoint;

    public int NumberOfEnemies1;
    public int NumberOfEnemies2;

    [SerializeField]
    public int Wave1Enemies1;
    public int Wave2Enemies1;
    public int Wave2Enemies2;
    public int Wave3Enemies1;
    public int Wave3Enemies2;

    public int numWave = 1;
    public int totalEnemies;

    private TimerScript timerScript;

    void Start() {

        timerScript = GetComponent<TimerScript>();

        NumberOfEnemies1 = Wave1Enemies1;
        if (!isTesting && !NetworkManager.Singleton.IsHost)
            return;
        spawnPoint = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }
        if (numWave == 1) {
            NumberOfEnemies1 = Wave1Enemies1;
            NumberOfEnemies2 = 0;
            totalEnemies = NumberOfEnemies1;
        }
        else if (numWave == 2) {
            NumberOfEnemies1 = Wave2Enemies1;
            NumberOfEnemies2 = Wave2Enemies2;
            totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
        }
        else if (numWave == 3) {
            NumberOfEnemies1 = Wave3Enemies1;
            NumberOfEnemies2 = Wave3Enemies2;
            totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
        }

        for (int i = 0; i < NumberOfEnemies1; i++) {

            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemyInstance = Instantiate(enemy1, randomPoint.transform);
            var instanceNetworkObject = enemyInstance.GetComponent<NetworkObject>();
            instanceNetworkObject.Spawn();
        }

        for (int i = 0; i < NumberOfEnemies2; i++) {

            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemyInstance = Instantiate(enemy2, randomPoint.transform);
            var instanceNetworkObject = enemyInstance.GetComponent<NetworkObject>();
            instanceNetworkObject.Spawn();
        }
    }

    private void Update() {
        if (totalEnemies <= 0) {
            numWave++;
            timerScript.enabled = true;

            //WinLoseHandler.Instance.UpdateGameState(GameState.Won);
        }
    }
}
