
using UnityEngine;
using UnityEngine.AI;

public class ControlPlayer : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

    public NavMeshAgent agent;
    public Vector3 destination = new Vector3(19f, 0.89f, 19f); // Destination finale

    void Start() {

        InvokeRepeating(nameof(FollowTarget), 1f, 5f);
    }

    void FollowTarget()
    {
        agent.SetDestination(destination);
    }
}
