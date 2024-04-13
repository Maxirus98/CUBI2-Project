using Unity.Netcode;
using UnityEngine;
using TMPro;

public class SpawnScript : MonoBehaviour {

    public bool isTesting = false;

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
    public AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {

        audioSource.PlayOneShot(SoundManager.Instance.waveStartSound);
        audioSource.Play();
        timerScript.enabled = false;

        NumberOfEnemies1 = Wave1Enemies1Normal + Wave1Enemies1SandMan + Wave1Enemies1Pet;

        if (!isTesting && !NetworkManager.Singleton.IsHost)
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
            totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
        }
        else if (numWave == 2) {
            NumberOfEnemies1Normal = Wave2Enemies1Normal;
            NumberOfEnemies1SandMan = Wave2Enemies1SandMan;
            NumberOfEnemies1Pet = Wave2Enemies1Pet;

            NumberOfEnemies1 = Wave2Enemies1Normal + Wave2Enemies1SandMan + Wave2Enemies1Pet;
            NumberOfEnemies2 = Wave2Enemies2;
            totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
        }
        else if (numWave == 3) {
            NumberOfEnemies1Normal = Wave3Enemies1Normal;
            NumberOfEnemies1SandMan = Wave3Enemies1SandMan;
            NumberOfEnemies1Pet = Wave3Enemies1Pet;

            NumberOfEnemies1 = Wave3Enemies1Normal + Wave3Enemies1SandMan + Wave3Enemies1Pet;
            NumberOfEnemies2 = Wave3Enemies2;
            totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
        }

        for (int i = 0; i < NumberOfEnemies1Normal; i++) {
            //Debug.Log("Ennemi" + i);
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy1NormalInstance = Instantiate(enemy1Normal, randomPoint.position, randomPoint.rotation);

            // Changement material EN DEV

            /*int randomInt = GetRandom();

            if (randomInt == 1) {
                ChangeMaterial(enemy1Instance, CouleurEnemySandMan);
                EditTag(enemy1Instance, CouleurEnemySandMan);
            }
            else if (randomInt == 2) {
                ChangeMaterial(enemy1Instance, CouleurEnemyPet);
                EditTag(enemy1Instance, CouleurEnemyPet);
            }*/

            // Spawn

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
            //Debug.Log("Nouvel ennemi2");
            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            var enemy2Instance = Instantiate(enemy2, randomPoint.position, randomPoint.rotation);


            // Spawn

            var instanceNetworkObject2 = enemy2Instance.GetComponent<NetworkObject>();
            instanceNetworkObject2.Spawn(true);
        }
    }

    public void Update() {
        
        if (totalEnemies <= 0) {
            Debug.Log("Plus d'ennemis");
            if (numWave == 1) {
                numWave = 2;
                totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
                print("wave 1 : " + totalEnemies);
                GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(SoundManager.Instance.waveEndSound);
            }
            else if (numWave == 2) {
                numWave = 3;
                totalEnemies = NumberOfEnemies1 + NumberOfEnemies2;
                print(totalEnemies);
                GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(SoundManager.Instance.waveEndSound);
            }
            /*else if (numWave == 3) {
                WinLoseHandler.Instance.UpdateGameState(GameState.Won);
            }*/
            
            //Debug.Log(timerScript.enabled);
            timerScript.enabled = true;
            //Debug.Log(timerScript.enabled);


            // WinLoseHandler.Instance.UpdateGameState(GameState.Won);
        }
    }

    private void OnDisable()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void EnableSpawnScript(TimerScript pTimer)
    {
        timerScript = pTimer;
        enabled = true;
    }

    private void ChangeMaterial(GameObject enemy, Material newMaterial) {
        Renderer enemyRend = enemy.GetComponent<Renderer>();
        if (enemyRend != null && newMaterial != null){
            enemyRend.material = newMaterial;
        }
        else {
            Debug.LogWarning("Renderer ou matériau manquant");
        }
    }

    private int GetRandom() {
        int aleatoire = Random.Range(1, 3);
        return aleatoire;

    }

    private void EditTag(GameObject enemy, Material newMaterial) {
        if (newMaterial == CouleurEnemySandMan) {
            enemy.tag = "EnemyForSandMan";
        }
        else if (newMaterial == CouleurEnemyPet) {
            enemy.tag = "EnemyForPet";
        }
    }
}
