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
    Vector3 destination = new(-63.45f, 1.5f, 32.46f);; // Destination finale

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
        destination = new Vector3(-63.45f, 1.5f, 32.46f);
        if (areSpeedValuesChosen) {
            agent.speed = Random.Range(Enemy1SpeedMin, Enemy1SpeedMin);
        }
        else if (!areSpeedValuesChosen) {
            if (agent.CompareTag("Enemy")) {
                print(agent.tag);
                print(Enemy1Speed1);
                agent.speed = Enemy1Speed1;
            }
            else if (agent.CompareTag("EnemySandMan")) {
                print(agent.tag);
                print(Enemy1Speed2);
                agent.speed = Enemy1Speed2;
            }
            else if (agent.CompareTag("EnemyPet")) {
                print(agent.tag);
                print(Enemy1Speed3);
                agent.speed = Enemy1Speed3;
            }
        }
        sandMan = GameObject.Find("SandmanModel").transform;
        pet = GameObject.Find("PetModel").transform;
        SpawnPointOfAgent = GetSpawnPoint();

        if (SpawnPointOfAgent == 1) {
            pathSpawnPoint1 = SetPathList1(pathSpawnPoint1);
            pathRandom = Random.Range(0, pathSpawnPoint1.Count);
            path = pathSpawnPoint1[pathRandom];
        }
        else if (SpawnPointOfAgent == 2) {
            pathSpawnPoint2 = SetPathList2(pathSpawnPoint2);
            pathRandom = Random.Range(0, pathSpawnPoint2.Count);
            path = pathSpawnPoint2[pathRandom];
        }
        else if (SpawnPointOfAgent == 3) {
            pathSpawnPoint3 = SetPathList3(pathSpawnPoint3);
            pathRandom = Random.Range(0, pathSpawnPoint3.Count);
            path = pathSpawnPoint3[pathRandom];
        }
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

        if (distAgentSandman >= distAgentPet) {
            closestLayer = whatIsPet;
            closestPlayer = pet.transform;
        }
        else if (distAgentPet >= distAgentSandman) {
            closestLayer = whatIsSandman;
            closestPlayer = sandMan.transform;
        }

        playerInRange = distAgentSandman <= rangeVue || distAgentPet <= rangeVue;
        playerInAttackRange = distAgentSandman <= rangeAttack || distAgentPet <= rangeAttack;

        if (!playerInRange && !playerInAttackRange) {
 
            ToKid();
        }
        if (playerInRange && !playerInAttackRange) {
            ToPlayer(closestPlayer);
        }
        if (playerInRange && playerInAttackRange) {
            AttackPlayer(closestPlayer);
        }

        anim.SetBool("Attacking", playerInAttackRange);
    }

    void ToKid() {
        if ((agent.transform.position - path[numDestination]).sqrMagnitude < 10f && (numDestination < (path.Count - 1))) {
                
                numDestination++;
                agent.SetDestination(path[numDestination]);
        }
    }

    void ToPlayer(Transform player) {
        agent.SetDestination(player.position);
    }

    void AttackPlayer(Transform player) {
        agent.SetDestination(player.position); // Attaque corps à corps
        transform.LookAt(player);
    }

    List<List<Vector3>> SetPathList1(List<List<Vector3>> list) {
        List<Vector3> path1 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(3.16f, -0.3101822f, 148.04f),
            new Vector3(4.4f, -0.3101822f, 94.1f),
            new Vector3(6.3f, -0.3101822f, 53.5f),
            destination
        };

        List<Vector3> path2 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(-0.2f, -0.3101822f, 141.1f),
            new Vector3(-4f, -0.3101822f, 92.1f),
            new Vector3(-13.6f, -0.3101822f, 72.6f),
            new Vector3(-6.1f, -0.3101822f, 49f),
            destination
        };

        List<Vector3> path3 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(1.8f, -0.3101822f, 145.5f),
            new Vector3(-7.3f, -0.3101822f, 84.8f),
            new Vector3(-62.5f, -0.3101822f, 78.1f),
            destination
        };

        List<Vector3> path4 = new() {
            new Vector3(12.7f, 1.5f, 163.8f),
            new Vector3(-24.6f, -0.3101822f, 151.7f),
            new Vector3(-48.4f, -0.3101822f, 107.8f),
            destination
        };

        list.Add(path1);
        list.Add(path2);
        list.Add(path3);
        list.Add(path4);


        return list;
    }

    List<List<Vector3>> SetPathList2(List<List<Vector3>> list) {
        List<Vector3> path1 = new() {
            new Vector3(-81.55f, -1.5f, 163.8f),
            new Vector3(-26.2f, -0.3101822f, 149.2f),
            new Vector3(-49.5f, -0.3101822f, 111.1f),
            destination
        };

        List<Vector3> path2 = new() {
            new Vector3(-81.55f, -1.5f, 163.8f),
            new Vector3(-57.9f, -0.3101822f, 110.3f),
            destination
        };


        List<Vector3> path3 = new() {
            new Vector3(-81.55f, -1.5f, 163.8f),
            new Vector3(-71.3f, -0.3101822f, 147.3f),
            new Vector3(-68f, -0.3101822f, 99.1f),
            destination
        };

        List<Vector3> path4 = new() {
            new Vector3(-81.55f, -1.5f, 163.8f),
            new Vector3(-89.2f, -0.3101822f, 154.7f),
            new Vector3(-67f, -0.3101822f, 95.3f),
            destination
        };

        list.Add(path1);
        list.Add(path2);
        list.Add(path3);
        list.Add(path4);


        return list;
    }

    List<List<Vector3>> SetPathList3(List<List<Vector3>> list) {
        List<Vector3> path1 = new() {
            new Vector3(-129.8f, -1.5f, 163.8f),
            new Vector3(-93.7f, -0.3101822f, 138.4f),
            new Vector3(-72.5f, -0.3101822f, 100.2f),
            destination
        };

        List<Vector3> path2 = new() {
            new Vector3(-129.8f, -1.5f, 163.8f),
            new Vector3(-119.3f, -0.3101822f, 117.7f),
            new Vector3(-75.7f, -0.3101822f, 96.4f),
            destination
        };


        List<Vector3> path3 = new() {
            new Vector3(-129.8f, -1.5f, 163.8f),
            new Vector3(-109.3f, -0.3101822f, 110.5f),
            new Vector3(-86.6f, -0.3101822f, 88.5f),
            new Vector3(-109.6f, -0.3101822f, 37.3f),
            destination
        };

        List<Vector3> path4 = new() {
            new Vector3(-129.8f, -1.5f, 163.8f),
            new Vector3(-120.8f, -0.3101822f, 110.2f),
            new Vector3(110.2f, -0.3101822f, 47f),
            destination
        };

        list.Add(path1);
        list.Add(path2);
        list.Add(path3);
        list.Add(path4);


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
}