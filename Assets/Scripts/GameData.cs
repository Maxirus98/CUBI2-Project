using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<TurretData> turrets = new List<TurretData>();

    public class TurretData
    {
        public string towerIndex; // L'index des tours
        public bool isBuilt; // Si la tour est construite ou détruite

        public TurretData(string towerIndex, bool isBuilt)
        {
            this.towerIndex = towerIndex;
            this.isBuilt = isBuilt;
        }
    }

    // Ajoute ou met à jour les données de la tour lorsqu'elle est construite ou détruite
    public void UpdateTurretData(string towerIndex, bool isBuilt)
    {
        // vérifie si la tour existe déjà dans la liste
        var existingTower = turrets.Find(turret => turret.towerIndex == towerIndex);
        if (existingTower != null)
        {
            // met à jour les données de la tour
            existingTower.isBuilt = isBuilt;
        }
        else
        {
            // ajoute une nouvelle tour à la liste
            turrets.Add(new TurretData(towerIndex, isBuilt));
        }
    }
}



