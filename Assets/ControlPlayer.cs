
using UnityEngine;
using UnityEngine.AI;

public class ControlPlayer : MonoBehaviour
{
    // Script 'test' pour déplacer l'ia avec un clic sur le terrain
    public Camera cam;

    public NavMeshAgent agent;

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                agent.SetDestination(hit.point);
            }

        }
    }
}
