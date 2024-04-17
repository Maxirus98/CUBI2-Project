using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Management : MonoBehaviour {
    public NavMeshAgent agent;
    public Transform sandMan;
    public Transform pet;

    public Transform closestPlayer;
    public GameObject[] players;

    public float distAgentSandman, distAgentPet, distTower;

    [SerializeField]
    float Enemy2Speed;
    float arret = 3f;
    float chargeSpeed = 10;

    [SerializeField]
    Vector3 destination;

    [SerializeField]
    float rangeVue, rangeAttack, rangeTowerAttack;
    bool playerInRange, playerInAttackRange, towerInAttackRange;

    private float distTourEnemy2;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy2Speed;

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
    }

    void Start() {
        agent.stoppingDistance = 0;
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        InvokeRepeating(nameof(ToKid), 1f, 5f);
    }

    public void Update() {
        distAgentSandman = (agent.transform.position - sandMan.position).sqrMagnitude;
        distAgentPet = (agent.transform.position - pet.position).sqrMagnitude;
       

        if (distAgentSandman >= distAgentPet) {
            closestPlayer = pet.transform;
        }
        else if (distAgentPet >= distAgentSandman) {
            closestPlayer = sandMan.transform;
        }

        playerInRange = distAgentSandman <= rangeVue || distAgentPet <= rangeVue;
        playerInAttackRange = distAgentSandman <= rangeAttack || distAgentPet <= rangeAttack;

        if (!playerInRange && !playerInAttackRange && !towerInAttackRange) {
            ToKid();
        }
        if (playerInRange && !playerInAttackRange && !towerInAttackRange) {
            ToPlayer(closestPlayer);
        }
        if (playerInRange && playerInAttackRange && !towerInAttackRange) {
            AttackPlayer(closestPlayer);
        }
    }

    void OnTriggerStay(Collider col) {
        if (col.CompareTag("Tower") && col.GetComponent<Turret>().isBuilt) {
            //distTourEnemy2 = (agent.transform.position - col.transform.position).sqrMagnitude;
            print("Entrée");
            towerInAttackRange = true;
            agent.SetDestination(col.transform.position);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Tower")) {
            print("Sortie");
            towerInAttackRange = false;
        }
    }

    void ToKid() {
        agent.SetDestination(destination);
    }

    void ToPlayer(Transform player) {
        agent.SetDestination(player.position);
    }

    void AttackPlayer(Transform player) {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
}
