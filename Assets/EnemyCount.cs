using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI enemyLabel;
    public SpawnScript spawnScript;
    public TextMeshProUGUI waveCountText;

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

    public IEnumerator ShowWave(int waveNum)
    {
        waveCountText.transform.parent.gameObject.SetActive(true);
        waveCountText.text = $"Vague {waveNum}";
        yield return new WaitForSeconds(2);
        waveCountText.transform.parent.gameObject.SetActive(false);
    }
}
