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
/// Script permettant de determiner si les joueurs ont gagnes ou perdus
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
                EndGame("Partie perdue! Les cauchemars ont atteint la porte et réveillé Sophie-Rose.", SoundManager.Instance.lostSound);
                break;
            case GameState.Won:
                winCanvas.SetActive(true);
                EndGame("Partie Gagnée! Vous avez éliminé tous les cauchemars. La petite Sophie-Rose fait de beaux rêves.", SoundManager.Instance.winSound);
                break;
            case GameState.HpLost:
                loseCanvas.SetActive(true);
                EndGame("Partie perdue! Un des défenseurs des rêves est tombé. La petite Sophie-Rose se réveille.", SoundManager.Instance.lostSound);
                break;
            default:
                break;
        }
    }

    private void EndGame(string text, AudioClip oneShotClip)
    {
        GameData.Instance.UpdateAllTowersData();
        var turretBuiltCount = GameData.Instance.turrets.FindAll(t => t.isBuilt).Count;

        endGameText.text = text + $"\n Vous avez construit {turretBuiltCount} tours. Elles seront détruites la prochaine partie";
        AudioSource.PlayClipAtPoint(oneShotClip, transform.position);
        endGameCanvas.SetActive(true);  
    }
}
