using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo; // La tourelle sur le cube

    public void BuildTurret(GameObject turretPrefab)
    {
        turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
    }

}
