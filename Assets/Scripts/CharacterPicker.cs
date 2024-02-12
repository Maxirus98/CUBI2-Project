using Unity.Netcode;

public class CharacterPicker : NetworkBehaviour
{
    private readonly NetworkVariable<int> netIndex = new();
    private readonly int[] indexes = { 0, 1 };
    private int modelIndex = 0;

    private void Awake()
    {
        // Subscribing to a change event. This is how the owner will change its color.
        // Could also be used for future color changes
        netIndex.OnValueChanged += OnValueChanged;
    }

    public override void OnDestroy()
    {
        netIndex.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(int prev, int next)
    {
        ActivateModel();
    }

    public override void OnNetworkSpawn()
    {
        if(IsHost)
        {
            ActivateModel();
            UpdateX(OwnerClientId <= 0 ? -4 : 4);
        } 

        // Take note, RPCs are queued up to run.
        // If we tried to immediately set our color locally after calling this RPC it wouldn't have propagated
        if (IsOwner)
        {
            modelIndex = (int) OwnerClientId;
            CommitNetworkIndexServerRpc(GetNextIndex());
        }
        else
        {
            ActivateModel();
        }
    }

    private void UpdateX(float x)
    {
        var pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }

    private void ActivateModel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(netIndex.Value).gameObject.SetActive(true);
    }

    [Rpc(SendTo.Server)]
    private void CommitNetworkIndexServerRpc(int index)
    {
        netIndex.Value = index;
    }

    private int GetNextIndex()
    {
        return indexes[modelIndex++ % indexes.Length];
    }
}
