using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static GameData;
using static PauseMenu;

public class GameManager : Singleton<GameManager>
{
    public string CurrentLevelName { get; set; }
    
    [SerializeField]
    private GameObject[] systemPrefabs;

    [SerializeField]
    private GameObject gameMenu;
    private List<AsyncOperation> loadOperations = new List<AsyncOperation>();
    private List<GameObject> instanceSystemPrefabsKept = new List<GameObject>();


    public GameData gameData;

    void Start()
    {
        CurrentLevelName = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(this);
        KeepSystemPrefabs();
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

    public void ToggleGameMenu()
    {
        gameMenu.SetActive(!gameMenu.activeInHierarchy);
    }

    public void ReturnToMainMenu()
    {
        RelayConnectionManager.Instance.DisconnectFromServer();
    }

    // Application.Quit() ne fonctionne pas dans l'éditeur, seulement dans le jeu build.
    // TODO: Vérifier que le jeu n'est pas en train de sauvegarder avant de fermer
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelAsync(string levelName)
    {
        Time.timeScale = 1f;
        for (int i = 0;i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
        if (CurrentLevelName.Equals(levelName))
        {
            Debug.Log($"On ne peut pas charger la même scène 2 fois: {levelName}");
            return;
        }

        CurrentLevelName = levelName;
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if (loadSceneAsync == null)
        {
            Debug.Log($"Une erreur est surevenue lors du chargement de la scène: {levelName}");
            return;
        }

        loadSceneAsync.completed += OnLoadSceneComplete;
        loadOperations.Add(loadSceneAsync);
    }

    private void OnLoadSceneComplete(AsyncOperation ao)
    {

        // Debug.Log("Scene : " + CurrentLevelName);
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

    // lorsque le joueur revient au jeu
    public void LoadGame() {
        string path = Application.persistentDataPath + "/turrets.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            TurretDataList loadedData = JsonUtility.FromJson<TurretDataList>(json);
            Debug.Log("Loaded data: " + json);

            ApplyLoadedData(loadedData.turrets);
        }
        Debug.Log("loading game...");
    }

    public void ApplyLoadedData(List<TurretData> loadedTurrets) {
        var towersParent = GameObject.Find("Tower").transform;
        foreach (var turretData in loadedTurrets) {
            Transform towerTransform = towersParent.Find(turretData.towerIndex);
            if (towerTransform != null) {
                Turret turretComponent = towerTransform.GetComponent<Turret>();
                if (turretComponent != null) {
                    turretComponent.isBuilt = turretData.isBuilt;
                    if (turretComponent.isBuilt) {
                        turretComponent.DestroyTowerServerRpc();
                    }
                }
            }

        }
    }
}
