using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterPicker : NetworkBehaviour
{
    [SerializeField]
    private List<Transform> models = new List<Transform>();
    private ushort modelIndex;

    public override void OnNetworkSpawn()
    {
        modelIndex = (ushort) (IsHost ? 0 : 1);
        if (!IsOwner)
        {
            Destroy(this);
        }

        AssignPlayerModel();
    }

    [Rpc(SendTo.Server)]
    public void SwitchCharacterRpc()
    {
        // var localClientId = NetworkManager.Singleton.LocalClientId;
        modelIndex = (ushort) (modelIndex == 0 ? 0 : 1);

        // Cherche le ClientObject
        // if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId, out var client)) return;

        if(IsOwner)
        {
            AssignPlayerModel();
        }
    }

    private void AssignPlayerModel()
    {
        //var playerPrefabTransform = NetworkManager.Singleton.LocalClient.PlayerObject.transform;
        //models[modelIndex].parent = playerPrefabTransform;
    }
}
