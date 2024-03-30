using Unity.Netcode;
using UnityEngine;
using TMPro;

public class SpawnScript : MonoBehaviour {
    public bool isTesting = false;

    [SerializeField]
    public GameObject enemy1;
    public GameObject enemy2;

    [SerializeField]
    Transform[] spawnPoint;

    public int numWave = 1;

    [Header("Nombre d'ennemis")]
    [SerializeField]
    public int Wave1Enemies1;
    public int Wave1Enemies2;
    public int Wave2Enemies1;
    public int Wave2Enemies2;
    public int Wave3Enemies1;
    public int Wave3Enemies2;




    [Header("Ne pas changer")]
    public int totalEnemies;
    private int NumberOfEnemies1;
    private int NumberOfEnemies2;

    private TimerScript timerScript;

    void Start() {
        /*print("ça marche");
        
        timerScript = GameObject.Find("Timer").GetComponent<TimerScript>();
        timerScript.enabled = false;

        //Debug.Log("TimerScript.enabled :" + timerScript.enabled);
        //Debug.Log("Num wave :" + numWave);
        //Debug.Log("NUMBER" + NumberOfEnemies1);
        NumberOfEnemies1 = Wave1Enemies1;
        //Debug.Log("NUMBER" + NumberOfEnemies1);
        //Debug.Log(NumberOfEnemies1);
        //Debug.Log(Wave1Enemies1);
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
            //Debug.Log("Ennemi" + i);
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy1Instance = Instantiate(enemy1, randomPoint.position, randomPoint.rotation);
            var instanceNetworkObject1 = enemy1Instance.GetComponent<NetworkObject>();
            instanceNetworkObject1.Spawn();
        }

        for (int j = 0; j < NumberOfEnemies2; j++) {
            //Debug.Log("Nouvel ennemi2");
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy2Instance = Instantiate(enemy2, randomPoint.position, randomPoint.rotation);
            var instanceNetworkObject2 = enemy2Instance.GetComponent<NetworkObject>();
            instanceNetworkObject2.Spawn();
    }*/
    }

    private void OnEnable() {
        print("ça marche");

        timerScript = GameObject.Find("Timer").GetComponent<TimerScript>();
        timerScript.enabled = false;

        //Debug.Log("TimerScript.enabled :" + timerScript.enabled);
        //Debug.Log("Num wave :" + numWave);
        //Debug.Log("NUMBER" + NumberOfEnemies1);
        NumberOfEnemies1 = Wave1Enemies1;
        //Debug.Log("NUMBER" + NumberOfEnemies1);
        //Debug.Log(NumberOfEnemies1);
        //Debug.Log(Wave1Enemies1);
        if (!isTesting && !NetworkManager.Singleton.IsHost)
            return;

        spawnPoint = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }
        if (numWave == 1) {
            NumberOfEnemies1 = Wave1Enemies1;
            NumberOfEnemies2 = Wave1Enemies2;
            totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
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
            //Debug.Log("Ennemi" + i);
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy1Instance = Instantiate(enemy1, randomPoint.position, randomPoint.rotation);
            var instanceNetworkObject1 = enemy1Instance.GetComponent<NetworkObject>();
            instanceNetworkObject1.Spawn();
        }

        for (int j = 0; j < NumberOfEnemies2; j++) {
            //Debug.Log("Nouvel ennemi2");
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy2Instance = Instantiate(enemy2, randomPoint.position, randomPoint.rotation);
            var instanceNetworkObject2 = enemy2Instance.GetComponent<NetworkObject>();
            instanceNetworkObject2.Spawn();
        }
    }

    public void Update() {
        
        if (totalEnemies <= 0) {
            Debug.Log("Plus d'ennemis");
            if (numWave == 1) {
                numWave = 2;
            }
            else if (numWave == 2) {
                numWave = 3;
            }

            Debug.Log(timerScript.enabled);
            timerScript.enabled = true;
            Debug.Log(timerScript.enabled);


            //WinLoseHandler.Instance.UpdateGameState(GameState.Won);
        }
    }
   
}
