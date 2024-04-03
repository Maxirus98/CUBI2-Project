using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Management : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

    public NavMeshAgent agent;
    public Transform sandMan;
    public Transform pet;
    public Transform closestPlayer;
    public GameObject[] players;

    public float distAgentSandman;
    public float distAgentPet;




    [SerializeField]
    float Enemy1Speed;

    [SerializeField]
    Vector3 destination; // Destination finale

    LayerMask whatIsSandman, whatIsPet;
    LayerMask closestLayer;

    // Ranges

    [SerializeField]
    float rangeVue, rangeAttack;
    bool playerInRange, playerInAttackRange;

    // For Attack animation
    private Animator anim;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy1Speed;

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        //closestPlayer = GameObject.Find("SandmanModel").transform;
        //players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start() { 
        anim = GetComponentInChildren<Animator>();
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        InvokeRepeating(nameof(ToKid), 1f, 5f);
        
    }

    public void Update() {

        distAgentSandman = (agent.transform.position - sandMan.position).sqrMagnitude;
        distAgentPet = (agent.transform.position - pet.position).sqrMagnitude;
        //Debug.Log("distAgentSandman :" + distAgentSandman);
        //Debug.Log("distAgentPet :" + distAgentPet);


        if (distAgentSandman >= distAgentPet) {
            closestLayer = whatIsPet;
            closestPlayer = pet.transform;
            //Debug.Log("Closest player (devrait être pet)" + closestLayer);
            //Debug.Log("Closest player (devrait être pet)", closestPlayer);
        }
        else if (distAgentPet >= distAgentSandman) {
            closestLayer = whatIsSandman;
            closestPlayer = sandMan.transform;
            //Debug.Log("Closest player (devrait être sandMan)" + closestLayer);
            //Debug.Log("Closest player (devrait être sandMan)" + closestPlayer);
        }

        playerInRange = distAgentSandman <= rangeVue || distAgentPet <= rangeVue;
        //playerInRange = Physics.CheckSphere(transform.position, rangeVue, closestLayer);
        playerInAttackRange = distAgentSandman <= rangeAttack || distAgentPet <= rangeAttack;
        //playerInAttackRange = Physics.CheckSphere(transform.position, rangeAttack, closestLayer);

        if (!playerInRange && !playerInAttackRange) {
            //Debug.Log("To kid");
            ToKid();
        }
        if (playerInRange && !playerInAttackRange) {
            //Debug.Log("Closest player :", closestPlayer);
            ToPlayer(closestPlayer);
        }
        if (playerInRange && playerInAttackRange) {
            //Debug.Log("Attacking");
            AttackPlayer(closestPlayer);
        }

        anim.SetBool("Attacking", playerInRange);
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