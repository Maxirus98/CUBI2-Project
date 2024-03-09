using UnityEngine;
using Unity.Netcode;

public class PlayerKo : NetworkBehaviour
{
    NetworkList<ulong> netKoPlayers = new();
    private static PlayerKo instance;

    public static PlayerKo Instance
    {
        get { return instance; }
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (IsInitialized)
        {
            Debug.LogError("Il n'y a qu'une seule instance possible.");
            return;
        }

        instance = this;
    }

    public override void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public override void OnNetworkSpawn()
    {
        netKoPlayers.OnListChanged += NetKoPlayers_OnListChanged;
    }

    private void NetKoPlayers_OnListChanged(NetworkListEvent<ulong> changeEvent)
    {
        if (changeEvent.Type == NetworkListEvent<ulong>.EventType.Add)
        {
            if(netKoPlayers.Count >= 2)
            {
                Debug.Log($"Players dead: {netKoPlayers.Count}");
                WinLoseHandler.Instance.UpdateGameState(GameState.Lost);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePlayerKoListServerRpc(ulong clientId)
    {
        netKoPlayers.Add(clientId);
        Debug.Log($"Size of ko players: {netKoPlayers.Count}");
    }
}
