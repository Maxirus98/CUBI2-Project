using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    private void Start()
    {
        var netObj = NetworkManager.Singleton.LocalClient.PlayerObject;
        var playerManager = netObj.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.DeactivateComponents();
        }
        NetworkLoader.LoadNetwork(NetworkLoader.Scene.Level_scene);
    }
}
