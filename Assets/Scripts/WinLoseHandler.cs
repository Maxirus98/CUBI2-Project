using TMPro;
using UnityEngine;

public enum GameState
{
    Running,
    DoorLost,
    Won,
    HpLost,
}

/// <summary>
/// Script permettant de d�terminer si les joueurs ont gagn�s ou perdus
/// </summary>
public class WinLoseHandler : Singleton<WinLoseHandler>
{
    [SerializeField]
    public GameObject endGameCanvas, winCanvas, loseCanvas;
    [SerializeField]
    private TextMeshProUGUI endGameText;

    public GameState CurrentGameState { get; set; }

    public void UpdateGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        switch (CurrentGameState)
        {
            case GameState.Running:
                break;
            case GameState.DoorLost:
                loseCanvas.SetActive(true);
                EndGame("Attention! Les cauchemars ont réussi leur objectif. La petite Rosalie se réveille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            case GameState.Won:
                winCanvas.SetActive(true);
                EndGame("Bravo! Le reve a été stabilisé, vous avez vaincu tous les cauchemars", SoundManager.Instance.winSound);
                break;
            case GameState.HpLost:
                loseCanvas.SetActive(true);
                EndGame("Attention! Un des défenseurs oniriques est tombé. La petite Sophie se réveille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            default:
                break;
        }
    }

    private void EndGame(string text, AudioClip oneShotClip)
    {
        GameData.Instance.UpdateAllTowersData();
        var turretBuiltCount = GameData.Instance.turrets.FindAll(t => t.isBuilt).Count;

        endGameText.text = text + $" Vous avez construit {turretBuiltCount} tours. Vous n'aurez pas accès a ces emplacements de construction pour les prochaines parties";
        AudioSource.PlayClipAtPoint(oneShotClip, transform.position);
        endGameCanvas.SetActive(true);  
    }
}
