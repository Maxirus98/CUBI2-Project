using System.Linq;
using UnityEngine;

public class DoorDetector : MonoBehaviour
{
    public float rangeRadius;
    public AudioSource audioSource;
    public bool canPlay = false;
    public GameObject warning;
    public GameObject iconContainer;

    void Update()
    {
        var hits = Physics.SphereCastAll(transform.position, rangeRadius, Vector3.forward, 0f);
        var hitEnemies = hits.Where(h => h.collider.CompareTag("Enemy") || h.collider.CompareTag("EnemyPet") || h.collider.CompareTag("EnemySandMan"));

        if(hitEnemies.Count() <= 0)
        {
            canPlay = true;
            audioSource.Stop();
        }
        
        if(canPlay && hitEnemies.Count() > 0)
        {
            canPlay = false;
            audioSource.Play();
        }

        iconContainer.SetActive(hitEnemies.Count() > 0);
        warning.SetActive(hitEnemies.Count() > 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}
