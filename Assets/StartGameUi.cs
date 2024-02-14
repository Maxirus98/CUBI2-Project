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

    private void EnablePlayerControllerRpc()
    {
        //var playerPrefab = NetworkManager.Singleton.LocalClient.PlayerObject;
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
}
