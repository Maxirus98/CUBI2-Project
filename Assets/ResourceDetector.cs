using UnityEngine;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField]
    private string ResourceTag;

    [SerializeField]
    private string ResourceText;

    [SerializeField]
    private PlayerStats playerStats;

    private void OnTriggerEnter(Collider other)
    {
        playerStats.IsNearResource = other.CompareTag(ResourceTag);
        playerStats.ToggleResourceText(ResourceText, other.CompareTag(ResourceTag));
    }

    private void OnTriggerStay(Collider other)
    {
        playerStats.IsNearResource = other.CompareTag(ResourceTag);
    }

    private void OnTriggerExit(Collider other)
    {
        playerStats.ToggleResourceText(ResourceText, false);
    }
}
