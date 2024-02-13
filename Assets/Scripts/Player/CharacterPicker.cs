using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPicker : NetworkBehaviour
{
    private readonly NetworkVariable<int> netIndex = new();
    private Button switchCharacterButton;
    private int modelIndex;

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
        Debug.Log("Received OnValueChanged");
        SwitchCharacter();
    }

    public override void OnNetworkSpawn()
    {
        if(IsHost)
        {
            SetInitialPosX(OwnerClientId <= 0 ? -4 : 4);
        }

        // Démarre la File (Queue) du RPC
        if (IsOwner)
        {
            modelIndex = (int)OwnerClientId;
            ActivateModel();
            Debug.Log($"Current Model Index {modelIndex} for Owner: {OwnerClientId}");
        }
    }

    private void Start()
    {
        switchCharacterButton = GameObject.Find("SwitchCharacter").GetComponent<Button>();
        if (IsHost)
        {
            switchCharacterButton.onClick.AddListener(delegate { CommitNetworkIndexServerRpc(); });
        } else
        {
            switchCharacterButton.gameObject.SetActive(false);
        }
    }

        private void SetInitialPosX(float x)
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

        transform.GetChild(modelIndex).gameObject.SetActive(true);
    }

    private void SwitchCharacter()
    {
        Debug.Log($"Current modelIndex is: {modelIndex}");
        for (int i = 0; i < transform.childCount; i++)
        {
            var go = transform.GetChild(i).gameObject;
            go.SetActive(!go.activeInHierarchy);
        }
    }

    [Rpc(SendTo.Server)]
    private void CommitNetworkIndexServerRpc()
    {
        var index = netIndex.Value == 0 ? 1 : 0;
        netIndex.Value = index;
    }
}
