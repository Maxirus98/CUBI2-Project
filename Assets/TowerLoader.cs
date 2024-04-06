using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
