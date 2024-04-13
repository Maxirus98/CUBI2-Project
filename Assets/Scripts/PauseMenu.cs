using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.Netcode;
using UnityEditor;

public class PauseMenu : NetworkBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameData gameData;

    private void Start()
    {
        GameManager.Instance.gameMenu = pauseMenuUI;
        GameManager.Instance.pauseMenu = this;
    }

    public void TogglePauseMenu()
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

    public void Recommencer()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SaveGameClientRpc();
        // set endGameCanvas active to false
        WinLoseHandler.Instance.endGameCanvas.SetActive(false);
        NetworkLoader.ReloadScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // lorsque le joueur quitte le jeu ou sauvegarde le jeu
    [ClientRpc]
    public void SaveGameClientRpc()
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


}