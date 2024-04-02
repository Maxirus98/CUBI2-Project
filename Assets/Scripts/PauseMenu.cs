using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameData gameData;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu called");
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SaveGame();
        // Sur le reseau
        GameManager.Instance.ReturnToMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // lorsque le joueur quitte le jeu ou sauvegarde le jeu
    public void SaveGame()
    {
        try
        {
            Debug.Log("Saving game...");
            gameData.UpdateAllTowersData();
            string json = JsonUtility.ToJson(gameData);
            File.WriteAllText(Application.persistentDataPath + "/turrets.json", json);
        }
        catch(Exception e)
        {
            Debug.LogError("Exception: " + e);
        }
    }

    [System.Serializable]
    public class TurretDataList
    {
        public List<GameData.TurretData> turrets = new List<GameData.TurretData>();
    }

    // lorsque le joueur revient au jeu
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/turrets.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            TurretDataList loadedData = JsonUtility.FromJson<TurretDataList>(json);
            Debug.Log("Loaded data: " + json);

            gameData.ApplyLoadedData(loadedData.turrets);
        }
        Debug.Log("loading game...");
    }
}