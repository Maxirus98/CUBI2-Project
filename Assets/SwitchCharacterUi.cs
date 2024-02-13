using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCharacterUi : NetworkBehaviour
{
    [SerializeField]
    private Button switchCharacterButton;

    public override void OnNetworkSpawn()
    {
        if(IsHost)
        {
        } else
        {
            switchCharacterButton.gameObject.SetActive(false);
        }
    }
}
