using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameData gameData;

    private void Start()
    {
        GameManager.Instance.gameMenu = pauseMenuUI;
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
        SaveGame();
        // set endGameCanvas active to false
        WinLoseHandler.Instance.endGameCanvas.SetActive(false);
        NetworkLoader.ReloadScene();
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


}