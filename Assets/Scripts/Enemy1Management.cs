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
    //private readonly GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

    [Header("Vos valeurs pour des tests, 1 pour chaque couleur")]
    public float Enemy1Speed1;
    public float Enemy1Speed2;
    public float Enemy1Speed3;

    [Header("Mettre vos valeurs après vos tests définitifs")]
    public float Enemy1SpeedMin;
    public float Enemy1SpeedMax;
    private float SpeedAleatoire;

    [Header("Cocher la case si vous voulez l'aléatoire")]
    public bool areSpeedValuesChosen = false;

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

        if (areSpeedValuesChosen) {
            agent.speed = Random.Range(Enemy1SpeedMin, Enemy1SpeedMin);
        }
        else if (!areSpeedValuesChosen) {
            if (agent.tag == "Enemy") {
                print(agent.tag);
                print(Enemy1Speed1);
                agent.speed = Enemy1Speed1;
            }
            else if (agent.tag == "EnemySandMan") {
                print(agent.tag);
                print(Enemy1Speed2);
                agent.speed = Enemy1Speed2;
            }
            else if (agent.tag == "EnemyPet") {
                print(agent.tag);
                print(Enemy1Speed3);
                agent.speed = Enemy1Speed3;
            }
        }
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

        anim.SetBool("Attacking", playerInAttackRange);
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


    /*public void OnTowerBuilt() {
        AvoidTowers();
    }

    void AvoidTowers() {
        foreach (GameObject tower in towers) {
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path)) {
                if (path.status == NavMeshPathStatus.PathComplete) {
                    return;
                }
            }

            Vector3 towerDirection = (tower.transform.position - transform.position).normalized;
            Vector3 avoidancePoint = tower.transform.position + towerDirection * 5f;
            agent.SetDestination(avoidancePoint);
        }
    }*/
}