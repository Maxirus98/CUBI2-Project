
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Apple;

public class ControlPlayer : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

    public NavMeshAgent agent;
    public Vector3 destination = new Vector3(19f, 0.89f, 19f); // Destination finale

    void Update() {

        agent.SetDestination(destination);
        
    }
}
