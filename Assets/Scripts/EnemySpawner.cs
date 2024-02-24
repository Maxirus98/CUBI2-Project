using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int CountEnemyAlive = 0;
    public Wave[] waves;
    public Transform START;
    public float waveRate = 0.2f;

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
                CountEnemyAlive++;
                yield return new WaitForSeconds(wave.rate); // attendre avant de spawn le prochain ennemi
            }
            while (CountEnemyAlive > 0)
            {
                yield return 0; // attendre que tous les ennemis soient morts
            }
            yield return new WaitForSeconds(waveRate); // attendre avant de spawn la prochaine vague
        }
    }
}
