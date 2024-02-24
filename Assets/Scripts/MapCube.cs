using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo; // La tourelle sur le cube

    [HideInInspector]
    public TurretData turretData;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        Debug.Log("Construire une tourelle: " + turretData.turretPrefab);
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
    }

    void OnMouseEnter()
    {

        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
