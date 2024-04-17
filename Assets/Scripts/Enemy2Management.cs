using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

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
    private Transform targetTower;
    private Rigidbody rb;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy2Speed;
        rb = GetComponent<Rigidbody>();
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

    private bool attackTower = true;


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Tower") && col.GetComponent<Turret>().isBuilt)
        {
            agent.enabled = false;
            targetTower = col.transform;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Tower") && col.GetComponent<Turret>().isBuilt && attackTower)
        {
            if((transform.position - targetTower.position).sqrMagnitude < 12)
            {
                targetTower.GetComponent<Turret>().DestroyTowerServerRpc();
                attackTower = false;
                agent.enabled = true;
                targetTower = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (targetTower != null)
        {
            MoveTowardsTower();
        }
    }

    void MoveTowardsTower()
    {
        var direction = (targetTower.position - transform.position).normalized;
        transform.LookAt(targetTower);
        rb.AddForce(direction * 5f, ForceMode.Acceleration);
    }

    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Tower")) {
            towerInAttackRange = false;
            agent.enabled = true;
            targetTower = null;
            attackTower = true;
        }
    }

    void ToKid() {
        if (!agent.enabled) return;
        agent.SetDestination(destination);
    }

    void ToPlayer(Transform player) {
        if (!agent.enabled) return;
        agent.SetDestination(player.position);
    }

    void AttackPlayer(Transform player) {
        if (!agent.enabled) return;
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
}
