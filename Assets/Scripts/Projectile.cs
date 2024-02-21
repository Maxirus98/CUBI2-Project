using UnityEngine;

public class Projectile : MonoBehaviour
{
    // [SerializeField] private AudioClip shootClip;

    public void Init(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
        Invoke(nameof(DestroyProjectile), 3);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}