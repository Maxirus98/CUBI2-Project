using System.Linq;
using UnityEngine;

public class DoorDetector : MonoBehaviour
{
    public float rangeRadius;
    public AudioSource audioSource;

    void Update()
    {
        var hits = Physics.SphereCastAll(transform.position, rangeRadius, Vector3.forward, 0f);
        var hitEnemies = hits.Where(h => h.collider.CompareTag("Enemy"));

        if(hitEnemies.Count() <= 0)
        {
            audioSource.Stop();
            return;
        }

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit " + hit.collider.name);
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
