using UnityEngine;

public class OnBuildDone : MonoBehaviour
{
    void Start()
    {
        var playerStats = FindObjectsOfType<PlayerStats>();
        foreach (var stat in playerStats) {
            stat.UseResource(5);
        }
    }
}
