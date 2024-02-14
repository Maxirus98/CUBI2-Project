using Unity.Netcode;
using UnityEngine;
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
            startButton.gameObject.SetActive(false);
        }
    }

    private void OnClickStart()
    {
        NetworkLoader.LoadNetwork(NetworkLoader.Scene.LevelMaxime);
        EnablePlayerControllerRpc();
        RelayConnectionManager.Instance.DisablePasswordUi();
    }

    // TODO: Enable non-host client's component
    [Rpc(SendTo.ClientsAndHost)]
    private void EnablePlayerControllerRpc()
    {
        var playerPrefab = NetworkManager.Singleton.LocalClient.PlayerObject;
        var playerController = playerPrefab.GetComponent<PlayerController>();
        var rb = playerPrefab.GetComponent<Rigidbody>();
        playerController.enabled = true;
        rb.useGravity = true;
    }
}
