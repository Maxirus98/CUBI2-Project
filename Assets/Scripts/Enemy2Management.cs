using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Management : MonoBehaviour {
    // Deplacer les ennemis vers un point fixe

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
    Vector3 destination; // Destination finale

    // Ranges

    [SerializeField]
    float rangeVue, rangeAttack, rangeTowerAttack;
    bool playerInRange, playerInAttackRange, towerInAttackRange;


    private bool isAttackCharging;
    private float chargeTimer = 0f;

    public List<Transform> towers = new List<Transform>();

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Enemy2Speed;

        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        //var towers =  FindObjectsOfType(Turret);
        //tower = GameObject.Find("Tower").transform;


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
        //distTower = (agent.transform.position - tower.position).sqrMagnitude;

        if (distAgentSandman >= distAgentPet) {
            closestPlayer = pet.transform;

        }
        else if (distAgentPet >= distAgentSandman) {
            closestPlayer = sandMan.transform;

        }

        playerInRange = distAgentSandman <= rangeVue || distAgentPet <= rangeVue;
        playerInAttackRange = distAgentSandman <= rangeAttack || distAgentPet <= rangeAttack;
        //towerInAttackRange = distTower <= rangeTowerAttack;

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
        //EN DEV


    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Tower") {
            towers.Add(col.transform);
            towerInAttackRange = true;
            var tower = col.GetComponent<Turret>();
            if (tower.isBuilt){
                AttackTower(tower.transform, tower);
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Tower") {
            towers.Remove(col.transform);
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
    void AttackTower(Transform tower, Turret towerAtt) {
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
                towerAtt.TakeDamage(1);

            }
        }
    }

    /*Transform getClosestTower() {

        return tour;
    }*/
}