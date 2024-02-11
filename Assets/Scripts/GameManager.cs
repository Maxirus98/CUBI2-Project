using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public string CurrentLevelName { get; set; }

    [SerializeField] 
    private GameObject[] systemPrefabs;
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();
    private List<GameObject> instanceSystemPrefabsKept = new List<GameObject>();

    void Start()
    {
        CurrentLevelName = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(this);
        KeepSystemPrefabs();
    }

    void Update()
    {
        // TODO: Change for new Input System
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

    }

    private void ToggleMenu()
    {
        var menu = instanceSystemPrefabsKept[0];
        menu.SetActive(!menu.activeInHierarchy);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (instanceSystemPrefabsKept == null) return;

        foreach (var prefabInstance in instanceSystemPrefabsKept)
        {
            if (prefabInstance != null) Destroy(prefabInstance);
        }

        instanceSystemPrefabsKept.Clear();
    }

    // TODO: Vérifier que le jeu n'est pas en train de sauvegarder avant de fermer
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        CurrentLevelName = levelName;

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if (loadSceneAsync == null)
        {
            Debug.Log($"Une erreur est survenue lors du chargement de la scène: {levelName}");
            return;
        }

        loadSceneAsync.completed += OnLoadSceneComplete;
        loadOperations.Add(loadSceneAsync);
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation unloadSceneAsync = SceneManager.UnloadSceneAsync(levelName);
        if (unloadSceneAsync == null)
        {
            print("error unloading scene : " + levelName);
            return;
        }
        unloadSceneAsync.completed += OnUnloadSceneComplete;
    }

    /// <summary>
    /// Méthode pour rendre les GameObjects de la liste systemPrefabs persistants à travers les scènes.
    /// </summary>
    private void KeepSystemPrefabs()
    {
        foreach (var go in systemPrefabs)
        {
            var clonePrefab = Instantiate(go);
            instanceSystemPrefabsKept.Add(clonePrefab);
            DontDestroyOnLoad(clonePrefab);
        }
    }

    private void OnLoadSceneComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            // Ici on peut aviser les composantes qui ont besoin de savoir que le level est load
            if (loadOperations.Count == 0)
            {
            }
        }

        Debug.Log($"Fin du chargement de la scène {CurrentLevelName}");
    }

    private void OnUnloadSceneComplete(AsyncOperation obj)
    {
        Debug.Log($"Fin du déchargement de la scène {CurrentLevelName}");
    }
}