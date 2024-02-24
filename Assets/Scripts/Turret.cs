using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret: MonoBehaviour
{
    public Transform target; // Cible de la tourelle
    public float range = 15f; // Port�e de la tourelle
    public string enemyTag = "Enemy"; // Tag des ennemis
    public Transform partToRotate; // Partie de la tourelle qui va tourner
    public float turnSpeed = 10f; // Vitesse de rotation de la tourelle

    // Start appel�e au d�marrage du jeu
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // M�thode pour mettre � jour la cible de la tourelle
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; // todo: Distance la plus courte
        GameObject nearestEnemy = null; 
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update appel�e � chaque frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position; // 
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
