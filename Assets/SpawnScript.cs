using Unity.Netcode;
using UnityEngine;
using TMPro;

public class SpawnScript : NetworkBehaviour {

    [SerializeField]
    public GameObject enemy1Normal;
    public GameObject enemy1SandMan;
    public GameObject enemy1Pet;
    public GameObject enemy2;

    [SerializeField]
    Transform[] spawnPoint;

    public int numWave = 1;

    [Header("Nombre d'ennemis")]
    [SerializeField]
    public int Wave1Enemies1Normal;
    public int Wave1Enemies1SandMan;
    public int Wave1Enemies1Pet;

    public int Wave1Enemies2;

    public int Wave2Enemies1Normal;
    public int Wave2Enemies1SandMan;
    public int Wave2Enemies1Pet;

    public int Wave2Enemies2;

    public int Wave3Enemies1Normal;
    public int Wave3Enemies1SandMan;
    public int Wave3Enemies1Pet;

    public int Wave3Enemies2;

    [Header("Couleurs des ennemis")]
    public Material CouleurEnemySandMan;
    public Material CouleurEnemyPet;

    [Header("Ne pas changer")]
    public int totalEnemies;
    private int NumberOfEnemies1, NumberOfEnemies2, NumberOfEnemies1Normal, NumberOfEnemies1SandMan, NumberOfEnemies1Pet;


    private TimerScript timerScript;
    private AudioSource audioSource;
    public EnemyCount enemyCount;
    private bool hasWon = false;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        timerScript.enabled = false;
        if (hasWon) return;
        StartCoroutine(enemyCount.ShowWave(numWave));
        audioSource.PlayOneShot(SoundManager.Instance.waveStartSound);
        audioSource.Play();

        NumberOfEnemies1 = Wave1Enemies1Normal + Wave1Enemies1SandMan + Wave1Enemies1Pet;

        if (!IsHost)
            return;

        spawnPoint = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }
        if (numWave == 1) {
            NumberOfEnemies1Normal = Wave1Enemies1Normal;
            NumberOfEnemies1SandMan = Wave1Enemies1SandMan;
            NumberOfEnemies1Pet = Wave1Enemies1Pet;

            NumberOfEnemies1 = Wave1Enemies1Normal + Wave1Enemies1SandMan + Wave1Enemies1Pet;
            NumberOfEnemies2 = Wave1Enemies2;
            SetTotalEnemiesClientRpc(NumberOfEnemies1 + NumberOfEnemies2);
        }
        else if (numWave == 2) {
            NumberOfEnemies1Normal = Wave2Enemies1Normal;
            NumberOfEnemies1SandMan = Wave2Enemies1SandMan;
            NumberOfEnemies1Pet = Wave2Enemies1Pet;

            NumberOfEnemies1 = Wave2Enemies1Normal + Wave2Enemies1SandMan + Wave2Enemies1Pet;
            NumberOfEnemies2 = Wave2Enemies2;
            SetTotalEnemiesClientRpc(NumberOfEnemies1 + NumberOfEnemies2);

        }
        else if (numWave == 3) {
            NumberOfEnemies1Normal = Wave3Enemies1Normal;
            NumberOfEnemies1SandMan = Wave3Enemies1SandMan;
            NumberOfEnemies1Pet = Wave3Enemies1Pet;

            NumberOfEnemies1 = Wave3Enemies1Normal + Wave3Enemies1SandMan + Wave3Enemies1Pet;
            NumberOfEnemies2 = Wave3Enemies2;
            SetTotalEnemiesClientRpc(NumberOfEnemies1 + NumberOfEnemies2);
        }

        for (int i = 0; i < NumberOfEnemies1Normal; i++) {
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
            var enemy1NormalInstance = Instantiate(enemy1Normal, randomPoint.position, randomPoint.rotation);
            var instanceNetworkObject1 = enemy1NormalInstance.GetComponent<NetworkObject>();
            instanceNetworkObject1.Spawn(true);
        }

        for (int j = 0; j < NumberOfEnemies1SandMan; j++) {
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy1SandManInstance = Instantiate(enemy1SandMan, randomPoint.position, randomPoint.rotation);

            var instanceNetworkObject2 = enemy1SandManInstance.GetComponent<NetworkObject>();
            instanceNetworkObject2.Spawn(true);
        }

        for (int k = 0; k < NumberOfEnemies1Pet; k++) {
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy1PetInstance = Instantiate(enemy1Pet, randomPoint.position, randomPoint.rotation);

            var instanceNetworkObject2 = enemy1PetInstance.GetComponent<NetworkObject>();
            instanceNetworkObject2.Spawn(true);
        }

        for (int l = 0; l < NumberOfEnemies2; l++) {
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy2Instance = Instantiate(enemy2, randomPoint.position, randomPoint.rotation);
            var instanceNetworkObject2 = enemy2Instance.GetComponent<NetworkObject>();
            instanceNetworkObject2.Spawn(true);
        }
    }

    public void Update() {
        if (hasWon) return;
        var noEnemies = totalEnemies <= 0;
        timerScript.enabled = noEnemies;
        enemyCount.ShowMonsterCount(!noEnemies);

        if (noEnemies)
        {
            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(SoundManager.Instance.waveEndSound);
            if(numWave >= 3)
            {
                hasWon = true;
                WinLoseHandler.Instance.UpdateGameState(GameState.Won);
            }

            if (!IsHost) return;
            if (numWave == 1)
            {
                SetNumWaveClientRpc(2);
                SetTotalEnemiesClientRpc(NumberOfEnemies1 + NumberOfEnemies2);

            }
            else if (numWave == 2)
            {
                SetNumWaveClientRpc(3);
                SetTotalEnemiesClientRpc(NumberOfEnemies1 + NumberOfEnemies2);
            }
        }

    }

    private void OnDisable()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        enemyCount.ShowMonsterCount(false);
    }

    public void EnableSpawnScript(TimerScript pTimer)
    {
        timerScript = pTimer;
        enabled = true;
    }

    [ClientRpc]
    public void SetTotalEnemiesClientRpc(int total)
    {
        totalEnemies = total;
        print("Total enemies: " + totalEnemies);
    }

    [ClientRpc]
    private void SetNumWaveClientRpc(int num)
    {
        numWave = num;
        print("Num wave: " + numWave);
    }
}
