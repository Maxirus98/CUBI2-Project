using System;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public static class NetworkLoader
{
    public enum Scene
    {
        MainMenu,
        ReloadingScene,
        Lobby,
        CharacterSelection,
        Level_scene,
    }

    private static UnityEngine.SceneManagement.Scene loadedScene;

    /// <summary>
    /// Charge la targetScene qui est partagée sur le serveur. Lorsque le client se connectera, il va charger la targetScene
    /// </summary>
    /// <param name="targetScene"></param>
    public static void LoadNetwork(Scene targetScene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
        NetworkManager.Singleton.SceneManager.OnSceneEvent += SceneManager_OnSceneEvent;
    }

    /// <summary>
    /// Handles processing notifications when subscribed to OnSceneEvent
    /// </summary>
    /// <param name="sceneEvent">class that has information about the scene event</param>
    private static void SceneManager_OnSceneEvent(SceneEvent sceneEvent)
    {
        switch (sceneEvent.SceneEventType)
        {
            case SceneEventType.LoadComplete:
                {
                    // We want to handle this for only the server-side
                    if (sceneEvent.ClientId == NetworkManager.ServerClientId)
                    {

                    }
                    break;
                }

            default:
                break;
        }
    }

    public static void ReloadScene()
    {
        var netObj = NetworkManager.Singleton.LocalClient.PlayerObject;
        var playerManager = netObj.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.DeactivateComponents();
        }

        NetworkManager.Singleton.SceneManager.LoadScene(Scene.ReloadingScene.ToString(), LoadSceneMode.Single);
    }
}