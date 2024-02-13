using Unity.Netcode;

public class CharacterPicker : NetworkBehaviour
{
    private readonly NetworkVariable<int> netIndex = new();
    private readonly int[] indexes = { 0, 1 };
    private int modelIndex = 0;

    private void Awake()
    {
        // Subscribing to a change event. This is how the owner will change its model.
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
            SetPosX(OwnerClientId <= 0 ? -4 : 4);
        } 

        // Démarre la File (Queue) du RPC
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

    private void SetPosX(float x)
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
