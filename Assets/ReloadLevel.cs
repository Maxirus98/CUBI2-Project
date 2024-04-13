using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    private void Start()
    {
        NetworkLoader.LoadNetwork(NetworkLoader.Scene.Level_scene);
    }
}
