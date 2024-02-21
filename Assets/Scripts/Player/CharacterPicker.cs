using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

// IMPORTANT: This component needs to be placed before Client Network Transform
public class CharacterPicker : NetworkBehaviour
{
    private NetworkVariable<int> netActiveModelIndex = new();

    private int activeModelIndex;
    private int unactiveModelIndex;
    private Button switchCharacterButton;

    public override void OnNetworkSpawn()
    {
        Debug.Log("OnNetworkSpawn was called");
        netActiveModelIndex.OnValueChanged += OnModelValueChanged;
        activeModelIndex = IsHost ? 0 : 1;
        unactiveModelIndex = IsHost ? 1 : 0;
        var xPos = IsHost ? -4 : 4;

        if (IsOwner)
        {
            transform.GetChild(activeModelIndex).gameObject.SetActive(true);
            transform.GetChild(unactiveModelIndex).gameObject.SetActive(false);
            transform.position = Vector3.right * xPos + Vector3.up;
        } else
        {
            transform.GetChild(unactiveModelIndex).gameObject.SetActive(true);
            transform.GetChild(activeModelIndex).gameObject.SetActive(false);
            transform.position = Vector3.left * xPos + Vector3.up;
        }
    }

    private void Start()
    {
        StartCoroutine(FindSwitchButton());
    }

    private IEnumerator FindSwitchButton()
    {
        yield return new WaitForSeconds(1);
        switchCharacterButton = GameObject.Find("SwitchCharacter").GetComponent<Button>();

        if (IsHost)
        {
            switchCharacterButton.onClick.AddListener(SwitchCharacter);
        }
        else
        {
            EnableSwitchCharacterButtonRpc();   
        }
    }

    public override void OnDestroy()
    {
        netActiveModelIndex.OnValueChanged -= OnModelValueChanged;
    }

    private void OnModelValueChanged(int previousValue, int newValue)
    {
        activeModelIndex = IsHost ? newValue : previousValue;
        unactiveModelIndex = IsHost ? previousValue : newValue;

        if (IsOwner)
        {
            transform.GetChild(activeModelIndex).gameObject.SetActive(true);
            transform.GetChild(unactiveModelIndex).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(unactiveModelIndex).gameObject.SetActive(true);
            transform.GetChild(activeModelIndex).gameObject.SetActive(false);
        }
    }

    private void SwitchCharacter()
    {
        netActiveModelIndex.Value = unactiveModelIndex;
    }

    [Rpc(SendTo.Server)]
    private void EnableSwitchCharacterButtonRpc()
    {
        switchCharacterButton.interactable = true;
    }

}
