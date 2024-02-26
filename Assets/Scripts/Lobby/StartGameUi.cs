using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameUi : MonoBehaviour
{
    private Button startButton;

    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStart);

        if (!NetworkManager.Singleton.IsHost)
        {
            startButton.interactable = false;
        }
    }

    private void OnClickStart()
    {
        NetworkLoader.LoadNetwork(NetworkLoader.Scene.Level_scene);
        RelayConnectionManager.Instance.DisablePasswordUi();
    }

    private void EnablePlayer(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        var playerPrefab = NetworkManager.Singleton.LocalClient.PlayerObject;
        var playerManager = playerPrefab.GetComponent<PlayerManager>();
        var rb = playerPrefab.GetComponent<Rigidbody>();
        playerManager.enabled = true;
        rb.useGravity = true;
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += EnablePlayer;
    }
}
