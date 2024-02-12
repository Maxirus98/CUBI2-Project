using Unity.Netcode;
using UnityEngine;

public class CharacterPicker : NetworkBehaviour
{
    [Rpc(SendTo.ClientsAndHost)]
    public void AssignPlayerModelRpc()
    {
        Debug.Log($"Called Assigned Player model from {NetworkManager.LocalClientId}");
        var modelIndex = NetworkManager.LocalClientId == 0 ? 0 : 1;
        var playerPrefabTransform = NetworkManager.Singleton.LocalClient.PlayerObject.transform;
        var newPosition = playerPrefabTransform.position;
        newPosition.x = modelIndex == 0 ? -4 : 4;

        playerPrefabTransform.position = newPosition;
        playerPrefabTransform.GetChild((int) modelIndex).gameObject.SetActive(true);
    }
}
