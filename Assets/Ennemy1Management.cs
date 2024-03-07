using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy1Management : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

    public NavMeshAgent agent;
    public Transform sandMan;
    public Transform pet;
    public GameObject[] players;

    [SerializeField]
    public Vector3 destination; // Destination finale

    public LayerMask whatIsPlayer;

    // Attaque on player
    bool isAttacked;

    // Ranges
    public float rangeVue, rangeAttack;
    bool playerInRange, playerInAttackRange;


    private void Awake() {
        agent = GetComponent<NavMeshAgent>();

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        //players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start() {
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        InvokeRepeating(nameof(ToKid), 1f, 5f);
    }

    private void Update() {
        playerInRange = Physics.CheckSphere(transform.position, rangeVue, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, rangeAttack, whatIsPlayer);

        if (!playerInRange && !playerInAttackRange) {
            ToKid();
        }
        if (playerInRange && !playerInAttackRange) {
            ToPlayer();
        }
        if (playerInRange && playerInAttackRange) {
            AttackPlayer();
        }

    }
    void ToKid() {
        agent.SetDestination(destination);
    }

    void ToPlayer() {
        agent.SetDestination(pet.position);
    }
    void AttackPlayer() {
        agent.SetDestination(pet.position); // Attaque corps à corps
        transform.LookAt(pet);

    }
}