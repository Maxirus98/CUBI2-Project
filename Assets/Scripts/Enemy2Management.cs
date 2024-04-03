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

    public float distAgentSandman, distAgentPet, distTower;

    [SerializeField]
    float Enemy2Speed;
    float arret = 3f;
    float chargeSpeed = 10;

    [SerializeField]
    Vector3 destination; // Destination finale

    // Ranges

    [SerializeField]
    float rangeVue, rangeAttack, rangeTowerAttack;
    bool playerInRange, playerInAttackRange, towerInAttackRange;

    private bool isAttackCharging;
    private float chargeTimer = 0f;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy2Speed;

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        tower = GameObject.Find("Tower").transform;
        //closestPlayer = GameObject.Find("SandmanModel").transform;
        //players = GameObject.FindGameObjectsWithTag("Player");
    }
    void Start() {
        agent.stoppingDistance = 0;
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        InvokeRepeating(nameof(ToKid), 1f, 5f);
    }

    public void Update() {

        distAgentSandman = (agent.transform.position - sandMan.position).sqrMagnitude;
        distAgentPet = (agent.transform.position - pet.position).sqrMagnitude;
        distTower = (agent.transform.position - tower.position).sqrMagnitude;

        if (distAgentSandman >= distAgentPet) {
            closestPlayer = pet.transform;

        }
        else if (distAgentPet >= distAgentSandman) {
            closestPlayer = sandMan.transform;

        }

        playerInRange = distAgentSandman <= rangeVue || distAgentPet <= rangeVue;
        playerInAttackRange = distAgentSandman <= rangeAttack || distAgentPet <= rangeAttack;
        towerInAttackRange = distTower <= rangeTowerAttack;

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
        if (towerInAttackRange) {
            AttackTower(tower);
            //AttackTower(getClosestTower());
        }

    }
    void ToKid() {
        agent.SetDestination(destination);
    }

    void ToPlayer(Transform player) {
        agent.SetDestination(player.position);
    }
    void AttackPlayer(Transform player) {
        agent.SetDestination(player.position); // Attaque corps ? corps
        transform.LookAt(player);

    }
    void AttackTower(Transform tower) {
        transform.LookAt(tower);
        if (!isAttackCharging) {
            isAttackCharging = true;
            chargeTimer = 0f;
            agent.isStopped = true;
        }
        else {
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= arret) {
                agent.isStopped = false;
                agent.speed = chargeSpeed;
                agent.SetDestination(tower.position);
            }
        }
    }

    /*Transform getClosestTower() {

        return tour;
    }*/
}