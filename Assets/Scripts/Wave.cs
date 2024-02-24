using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Garder les atributs des ennemis
[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public int count;
    public float rate;
}
