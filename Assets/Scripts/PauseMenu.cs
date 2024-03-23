using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

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
        SaveGame();
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // lorsque le joueur quitte le jeu ou sauvegarde le jeu
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(GameData.Instance, true);
        File.WriteAllText(Application.persistentDataPath + "/turrets.json", json);
        print("gamedata" + json);
    }

    // lorsque le joueur revient au jeu
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/turrets.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData.Instance.LoadDataFromJson(json);
        }
    }
}