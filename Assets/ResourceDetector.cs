using UnityEngine;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField]
    private string ResourceTag;

    [SerializeField]
    private PlayerStats playerStats;

    private void OnTriggerEnter(Collider other)
    {
        playerStats.IsNearResource = other.CompareTag(ResourceTag);
    }

    private void OnTriggerExit(Collider other)
    {
        playerStats.IsNearResource = other.CompareTag(ResourceTag);
    }
}