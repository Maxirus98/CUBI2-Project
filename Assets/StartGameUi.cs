using System;
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


    [Rpc(SendTo.ClientsAndHost)]
    private void EnablePlayerControllerRpc(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        var playerPrefab = NetworkManager.Singleton.LocalClient.PlayerObject;
        var connectedClients = NetworkManager.Singleton.ConnectedClientsIds;
        foreach (var item in connectedClients)
        {
            Debug.Log($"Connected client ids: {item}");
        }

        Debug.Log($"Player with id {NetworkManager.Singleton.LocalClient} can now move");
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            var playerController = player.GetComponent<PlayerController>();
            var rb = player.GetComponent<Rigidbody>();
            playerController.enabled = true;
            rb.useGravity = true;
        }
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += EnablePlayerControllerRpc;
    }
}
