using UnityEngine;

/// <summary>
/// Script dans CharacterSelection Scene pour desactiver le PlayerManager de tous les objets locaux et de les positionner
/// </summary>
public class CharacterValidation : MonoBehaviour
{
    public GameObject[] players;
    private int disabledPlayerCount = 0;

    private void Start()
    {
        disabledPlayerCount = 0;
    }

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (disabledPlayerCount >= 2) return;
        
        foreach (var player in players)
        {
            var playerManager = player.GetComponent<PlayerManager>();
            var rb = player.GetComponent<Rigidbody>();
            
            if (playerManager != null && playerManager.enabled)
            {
                disabledPlayerCount++;
                playerManager.enabled = false;
            }

            if(rb != null)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;

                var isSandman = player.transform.GetChild(0).gameObject.activeInHierarchy;
                var xPos = isSandman ? -4 : 4;
                player.transform.position = new Vector3(xPos, 1, 0);
                player.transform.rotation = Quaternion.Euler(0f, 165f, 0f);
            }
        }
    }
}
