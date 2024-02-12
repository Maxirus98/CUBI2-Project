using Unity.Netcode;
using UnityEngine;


public class PlayerVisual : NetworkBehaviour
{
    private MeshRenderer meshRenderer;
    public override void OnNetworkSpawn()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = OwnerClientId == 0 ? Color.white : Color.black;
    }
}
