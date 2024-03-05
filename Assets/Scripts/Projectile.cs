using UnityEngine;

public class Projectile : MonoBehaviour
{
    // [SerializeField] private AudioClip shootClip;
    [SerializeField] private ParticleSystem splashFx;

    public void Init(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
        Invoke(nameof(DestroyProjectile), 3);
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) return;
        var projectileModel =  transform.GetChild(0);
        projectileModel.gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        splashFx.Play();
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject, 0.5f);
    }

}