using Unity.Netcode;
using UnityEngine;

public class ReadyCheck : NetworkBehaviour
{
    [SerializeField] NetworkVariable<bool> netIsReady = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        netIsReady.OnValueChanged += OnReadyChanged;
    }

    private void OnReadyChanged(bool previousValue, bool newValue)
    {
        Debug.Log($"Player {OwnerClientId} isReady: {newValue}");
    }

    public bool IsReady { get => netIsReady.Value; set => netIsReady.Value = value; }
}
