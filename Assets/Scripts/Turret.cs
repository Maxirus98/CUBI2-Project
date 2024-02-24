using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> ennemies = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            ennemies.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Enemy")
        {
            ennemies.Remove(col.gameObject);
        }
    }
}
