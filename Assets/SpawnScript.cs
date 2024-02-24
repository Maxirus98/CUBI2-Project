using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    public GameObject ennemy;

    [SerializeField]
    Transform[] spawnPoint; 

    void Start()
    {
 

        var gameObject = new GameObject("EnnemyModel");
        spawnPoint = new Transform[transform.childCount];

        for (int i=0; i<transform.childCount; i++) {
            spawnPoint[i] = transform.GetChild(i);
        }

        for (int i = 0; i < 3; i++) {

            Transform randomPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            GameObject instantiated = Instantiate(ennemy);
            instantiated.transform.position = randomPoint.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
