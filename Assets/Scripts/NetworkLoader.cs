using Unity.Netcode;
using UnityEngine.SceneManagement;

public static class NetworkLoader
{
    public enum Scene
    {
        Menu,
        LoadingScene,
        Lobby,
        CharacterSelection,
        Level_scene
    }

    /// <summary>
    /// Charge la targetScene qui est partagée sur le serveur. Lorsque le client se connectera, il va charger la targetScene
    /// </summary>
    /// <param name="targetScene"></param>
    public static void LoadNetwork(Scene targetScene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
    }
}