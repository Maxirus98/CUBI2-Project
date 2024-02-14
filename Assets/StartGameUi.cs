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

    [Rpc(SendTo.Everyone)]
    private void EnablePlayerControllerRpc()
    {
        var playerController = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerController>();
        playerController.enabled = true;
    }
}
