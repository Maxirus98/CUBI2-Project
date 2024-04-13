using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    public float speed = 10f;
    private Transform target;
    private NetworkObject networkObject;

    private void Start()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitEnemy();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
        }
        else
        {
            if (IsHost)
            {
                networkObject.Despawn();
            }
        }
    }

    void HitEnemy()
    {
        var enemy = target.GetComponent<EnemyStats>();
        enemy.TakeDamage(1);
        if (IsHost)
        {
            networkObject.Despawn();
        }
    }
}