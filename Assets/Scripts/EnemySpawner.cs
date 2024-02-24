using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform START;
    public float waveRate = 3;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity); // pas de rotation
                yield return new WaitForSeconds(wave.rate); // attendre avant de spawn le prochain ennemi
            }
            yield return new WaitForSeconds(waveRate); // attendre avant de spawn la prochaine vague
        }
    }
}
