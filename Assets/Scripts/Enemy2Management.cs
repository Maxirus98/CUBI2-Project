using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Management : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

    public NavMeshAgent agent;
    public Transform sandMan;
    public Transform pet;
    public Transform closestPlayer;
    public GameObject[] players;

    public float distAgentSandman;
    public float distAgentPet;


    [SerializeField]
    public float Enemy2Speed;

    [SerializeField]
    public Vector3 destination; // Destination finale

    public LayerMask whatIsSandman, whatIsPet;
    public LayerMask closestLayer;

    // Ranges
    public float rangeVue, rangeAttack;
    bool playerInRange, playerInAttackRange;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy2Speed;

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        //closestPlayer = GameObject.Find("SandmanModel").transform;
        //players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start() {
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        InvokeRepeating(nameof(ToKid), 1f, 5f);
    }

    public void Update() {

        distAgentSandman = Vector3.Distance(agent.transform.position, sandMan.position);
        distAgentPet = Vector3.Distance(agent.transform.position, pet.position);
        Debug.Log("distAgentSandman :" + distAgentSandman);
        Debug.Log("distAgentPet :" + distAgentPet);


        if (distAgentSandman >= distAgentPet) {
            closestLayer = whatIsPet;
            closestPlayer = pet.transform;
            Debug.Log("Closest player (devrait être pet)" + closestLayer);
            Debug.Log("Closest player (devrait être pet)", closestPlayer);
        }
        else if (distAgentPet >= distAgentSandman) {
            closestLayer = whatIsSandman;
            closestPlayer = sandMan.transform;
            Debug.Log("Closest player (devrait être sandMan)" + closestLayer);
            Debug.Log("Closest player (devrait être sandMan)" + closestPlayer);
        }

        playerInRange = Physics.CheckSphere(transform.position, rangeVue, closestLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, rangeAttack, closestLayer);

        if (!playerInRange && !playerInAttackRange) {
            Debug.Log("To kid");
            ToKid();
        }
        if (playerInRange && !playerInAttackRange) {
            Debug.Log("Closest player :", closestPlayer);
            ToPlayer(closestPlayer);
        }
        if (playerInRange && playerInAttackRange) {
            Debug.Log("Attacking");
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