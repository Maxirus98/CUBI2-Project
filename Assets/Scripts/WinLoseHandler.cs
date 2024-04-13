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
    public GameObject endGameCanvas;
    [SerializeField]
    private TextMeshProUGUI endGameText;

    public GameState CurrentGameState {get; set;}

    public void UpdateGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        switch (CurrentGameState)
        {
            case GameState.Running:
                break;
            case GameState.DoorLost:
                EndGame("Attention! Les cauchemars ont r�ussi leur objectif. La petite Rosalie se r�veille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            case GameState.Won:
                EndGame("Bravo! Le r�ve a �t� stabilis�, vous avez vaincu tous les cauchemars", SoundManager.Instance.winSound);
                break;
            case GameState.HpLost:
                EndGame("Attention! Un des d�fenseurs oniriques est tomb�. La petite Sophie se r�veille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            default:
                break;
        }

        Invoke(nameof(StopGameLoop), 1f);
    }

    private void EndGame(string text, AudioClip oneShotClip)
    {
        GameData.Instance.UpdateAllTowersData();
        var turretBuiltCount = GameData.Instance.turrets.FindAll(t => t.isBuilt).Count;

        endGameText.text = text + $" Vous avez construit {turretBuiltCount} tours. Vous n'aurez pas acc�s � ces emplacements de construction pour les prochaines parties";
        AudioSource.PlayClipAtPoint(oneShotClip, transform.position);
        endGameCanvas.SetActive(true);
    }

    // TODO: Changer ceci car les sons vont aussi s'arr�ter
    private void StopGameLoop()
    {
        Time.timeScale = CurrentGameState == GameState.Running ? 1 : 0;
    }
}
