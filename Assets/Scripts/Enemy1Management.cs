using System.Collections.Generic;
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

    List<List<Vector3>> pathSpawnPoint1 = new();
    List<List<Vector3>> pathSpawnPoint2 = new();
    List<List<Vector3>> pathSpawnPoint3 = new();
    List<Vector3> path = new();
    private int numDestination;
    private float distSpawnPoint1;
    private float distSpawnPoint2;
    private float distSpawnPoint3;
    private float closestDistSP;
    private int SpawnPointOfAgent;
    private int pathRandom;


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
        SpawnPointOfAgent = GetSpawnPoint();
        pathSpawnPoint1 = SetPathList(pathSpawnPoint1);
        pathRandom = Random.Range(0, pathSpawnPoint1.Count);
        path = pathSpawnPoint1[pathRandom];


    }
    void Start() { 
        anim = GetComponentInChildren<Animator>();
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        numDestination = 0;
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
        
        if (SpawnPointOfAgent == 1) {
            print("SP : 1");
            print("dsit = " + (agent.transform.position - path[numDestination]).sqrMagnitude);
            print(agent.transform.position);
            print(path[numDestination]);
            print("distance " + ((agent.transform.position - path[numDestination]).sqrMagnitude < 10f));
            print("num " + (numDestination < path.Count));

            print("pitié : " + ((agent.transform.position - path[numDestination]).sqrMagnitude < 10f && (numDestination < path.Count)));
            

            if ((agent.transform.position - path[numDestination]).sqrMagnitude < 10f && (numDestination < (path.Count+1))) {
                print("ça marche");
                numDestination++;
                agent.SetDestination(path[numDestination]);
                

            }

        }

        else if (SpawnPointOfAgent == 2) {
            //print("SP : 2");
            agent.SetDestination(destination);

           /*pathRandom = Random.Range(0, pathSpawnPoint2.Count);
            path = pathSpawnPoint2[pathRandom];


            if ((agent.transform.position - path[numDestination]).sqrMagnitude < 1f && (numDestination < path.Count)) {
                agent.SetDestination(path[numDestination]);
                numDestination++;

            }*/

        }

        else if (SpawnPointOfAgent == 3) {
            //print("SP : 3");
            agent.SetDestination(destination);

           /*pathRandom = Random.Range(0, pathSpawnPoint3.Count);
            path = pathSpawnPoint3[pathRandom];


            if ((agent.transform.position - path[numDestination]).sqrMagnitude < 1f && (numDestination < path.Count)) {
                agent.SetDestination(path[numDestination]);
                numDestination++;

            }*/

        }

    }

    void ToPlayer(Transform player) {
        agent.SetDestination(player.position);
    }
    void AttackPlayer(Transform player) {
        agent.SetDestination(player.position); // Attaque corps à corps
        transform.LookAt(player);
    }
    //path1.Add(new Vector3(f, -0.3101822f, f));
    List<List<Vector3>> SetPathList(List<List<Vector3>> list) {
        List<Vector3> path1 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(3.16f, -0.3101822f, 148.04f),
            new Vector3(4.4f, -0.3101822f, 94.1f),
            new Vector3(6.3f, -0.3101822f, 53.5f),
            destination
        };

        List<Vector3> path2 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(3.16f, -0.3101822f, 148.04f),
            new Vector3(4.4f, -0.3101822f, 94.1f),
            new Vector3(-12.88f, -0.3101822f, 78.53f),
            new Vector3(-6.1f, -0.3101822f, 49f),
            (destination)
        };


        List<Vector3> path3 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(1.8f, -0.3101822f, 145.5f),
            new Vector3(-7.3f, -0.3101822f, 84.8f),
            new Vector3(-62.5f, -0.3101822f, 78.1f),
            destination
        };

        list.Add(path1);
        list.Add(path2);
        list.Add(path3);



        return list;
    }

    private int GetSpawnPoint() {
        distSpawnPoint1 = (agent.transform.position - new Vector3(12.7f, 1.5f, 163.8f)).sqrMagnitude;
        distSpawnPoint2 = (agent.transform.position - new Vector3(-81.55f, 1.5f, 163.8f)).sqrMagnitude;
        distSpawnPoint3 = (agent.transform.position - new Vector3(-129.8f, -1.3f, 163.8f)).sqrMagnitude;

        if ((distSpawnPoint1 < distSpawnPoint2) && (distSpawnPoint1 < distSpawnPoint3)) {
            return 1;
        }
        else if ((distSpawnPoint2 < distSpawnPoint1) && (distSpawnPoint2 < distSpawnPoint3)) {
            return 2;
        }
        else if ((distSpawnPoint3 < distSpawnPoint1) && (distSpawnPoint3 < distSpawnPoint2)) {
            return 3;
        }
        return -1;
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