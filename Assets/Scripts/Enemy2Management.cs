using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Management : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

    public NavMeshAgent agent;
    public Transform sandMan;
    public Transform pet;
    public Transform tower;
    public Transform closestPlayer;

    public GameObject[] players;

    public float distAgentSandman;
    public float distAgentPet;
    public float distTower;


    [SerializeField]
    public float Enemy2Speed;

    [SerializeField]
    public Vector3 destination; // Destination finale

    public LayerMask whatIsSandman, whatIsPet, whatIsTower;
    public LayerMask closestLayer;

    // Ranges
    public float rangeVue, rangeAttack;
    bool playerInRange, playerInAttackRange;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy2Speed;

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        tower = GameObject.Find("Tower").transform;
        closestPlayer = GameObject.Find("SandmanModel").transform;
        //players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start() {
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        InvokeRepeating(nameof(ToKid), 1f, 5f);
    }

    private void Update() {

        distAgentSandman = Vector3.Distance(agent.transform.position, sandMan.transform.position);
        distAgentPet = Vector3.Distance(agent.transform.position, pet.transform.position);
        distTower = Vector3.Distance(agent.transform.position, tower.transform.position);

        if (distAgentSandman >= distAgentPet) {
            closestLayer = whatIsPet;
            closestPlayer = pet.transform;
            Debug.Log(closestPlayer);
        }
        else if (distAgentPet >= distAgentSandman) {
            closestLayer = whatIsSandman;
            closestPlayer = sandMan.transform;
            Debug.Log(closestPlayer);
        }

        playerInRange = Physics.CheckSphere(transform.position, rangeVue, closestLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, rangeAttack, closestLayer);

        if (!playerInRange && !playerInAttackRange) {
            ToKid();
        }
        if (playerInRange && !playerInAttackRange) {
            ToPlayer(closestPlayer);
        }
        if (playerInRange && playerInAttackRange) {
            AttackPlayer(closestPlayer);
        }

    }
    void ToKid() {
        agent.SetDestination(destination);
    }

    void ToPlayer(Transform player) {
        agent.SetDestination(player.position);
    }
    void AttackPlayer(Transform player) {
        agent.SetDestination(player.position); // Attaque corps à corps
        transform.LookAt(player);

    }
}
