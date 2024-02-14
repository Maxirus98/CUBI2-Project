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
        NetworkLoader.LoadNetwork(NetworkLoader.Scene.LevelMaxime);
        RelayConnectionManager.Instance.DisablePasswordUi();
    }

    private void EnablePlayerController(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        var playerPrefab = NetworkManager.Singleton.LocalClient.PlayerObject;
        var playerController = playerPrefab.GetComponent<PlayerController>();
        var rb = playerPrefab.GetComponent<Rigidbody>();
        playerController.enabled = true;
        rb.useGravity = true;
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += EnablePlayerController;
    }
}
