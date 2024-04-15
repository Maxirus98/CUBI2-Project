using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI enemyLabel;
    public SpawnScript spawnScript;

    void Start()
    {
        spawnScript = FindObjectOfType<SpawnScript>();
        spawnScript.enemyCount = this;
    }

    void Update()
    {
        if(enemyCount.gameObject.activeInHierarchy)
        {
            enemyCount.text = spawnScript.totalEnemies.ToString();
        }
    }


    public void ShowMonsterCount(bool isSpawned)
    {
        enemyCount.gameObject.SetActive(isSpawned);
        enemyLabel.gameObject.SetActive(isSpawned);
    }

}
