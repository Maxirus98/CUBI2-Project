using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ReadyCheckUi : NetworkBehaviour
{
    [SerializeField]
    private Button startButton;
    private Button readyButton;
    private Image readyButtonImage;
    private ReadyCheck playerReadyCheck;
    private NetworkList<ulong> netReadyPlayerList = new();

    public override void OnNetworkSpawn()
    {
        netReadyPlayerList.OnListChanged += NetReadyPlayerList_OnListChanged;
    }

    private void NetReadyPlayerList_OnListChanged(NetworkListEvent<ulong> changeEvent)
    {
        if(changeEvent.Type == NetworkListEvent<ulong>.EventType.Add)
        {
            Debug.Log($"Added to list, Size: {netReadyPlayerList.Count}");
        }

        if (changeEvent.Type == NetworkListEvent<ulong>.EventType.Remove)
        {
            Debug.Log($"Remove from list, Size: {netReadyPlayerList.Count}");
        }

        if(IsHost)
        {
            startButton.interactable = netReadyPlayerList.Count >= 2;
        }
    }

    void Start()
    {
        readyButtonImage = GetComponent<Image>();
        readyButton = GetComponent<Button>();
        readyButton.onClick.AddListener(OnClickReadyRpc);
        OnClientConnected();
    }

    private void OnClientConnected()
    {
        playerReadyCheck = NetworkManager.LocalClient.PlayerObject.GetComponent<ReadyCheck>();
        Debug.Log("PlayerReadyCheck: " + playerReadyCheck);
    }
    
    private void OnClickReadyRpc()
    {
        playerReadyCheck.IsReady = !playerReadyCheck.IsReady;

        if(playerReadyCheck.IsReady)
        {
            readyButtonImage.color = Color.green;
            UpdatePlayerReadyListRpc();
        } else
        {
            readyButtonImage.color = Color.white;
            UpdatePlayerReadyListRpc(false);
        }

        
    }

    [Rpc(SendTo.Server)]
    private void UpdatePlayerReadyListRpc(bool adding = true)
    {
        if(adding)
        {
            netReadyPlayerList.Add(OwnerClientId);
        } else
        {
            netReadyPlayerList.Remove(OwnerClientId);
        }
        
    }
}
