using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    public TurretData selectedTurretData;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isCollider = Physics.Raycast(ray, out RaycastHit hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        // Construire une tourelle
                        mapCube.BuildTurret(selectedTurretData);
                    }
                    else
                    {
                        // todo: Améliorer la tourelle            
                    }
                }
            }
        }
    }

    // Sélectionner un type de tourelle
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
}


