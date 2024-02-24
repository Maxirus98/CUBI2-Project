using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject cannonPrefab;
    public bool isPlayerInRange = false;

    public List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            BuildCannon();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
        else if (col.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

    void BuildCannon()
    {
        if (cannonPrefab != null)
        {
            Vector3 buildPosition = transform.position + new Vector3(0, 4f, 0);
            GameObject canonInstance = Instantiate(cannonPrefab, buildPosition, Quaternion.Euler(0, 0, 90));
        }
    }
}
