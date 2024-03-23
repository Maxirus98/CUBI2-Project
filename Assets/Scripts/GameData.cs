using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public List<TurretData> turrets = new List<TurretData>();

    public static GameData Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTurrets();
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    private void InitializeTurrets()
    {
        for (int i = 1; i <= 18; i++)
        {
            turrets.Add(new TurretData("Tower" + i, false));
        }
        print("turrets" + turrets);
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
        print("hereQ");
    }

    public void LoadDataFromJson(string json)
    {
        GameData loadedData = JsonUtility.FromJson<GameData>(json);
        if (loadedData != null)
        {
            // copie les données des tours
            turrets = loadedData.turrets;
        }
    }

}