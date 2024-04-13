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
/// Script permettant de déterminer si les joueurs ont gagnés ou perdus
/// </summary>
public class WinLoseHandler : Singleton<WinLoseHandler>
{
    [SerializeField]
    private GameObject endGameCanvas;
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
                EndGame("Attention! Les cauchemars ont réussi leur objectif. La petite Rosalie se réveille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            case GameState.Won:
                EndGame("Bravo! Le rêve a été stabilisé, vous avez vaincu tous les cauchemars", SoundManager.Instance.winSound);
                break;
            case GameState.HpLost:
                EndGame("Attention! Un des défenseurs oniriques est tombé. La petite Sophie se réveille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            default:
                break;
        }

        // Arrête le jeu et tous les événements après 3 secondes
        Invoke(nameof(StopGameLoop), 3f);
    }

    private void EndGame(string text, AudioClip oneShotClip)
    {
        GameData.Instance.UpdateAllTowersData();
        var turretBuiltCount = GameData.Instance.turrets.FindAll(t => t.isBuilt).Count;

        endGameText.text = text + $" Vous avez construit {turretBuiltCount} tours. Vous n'aurez pas accès à ces emplacements de construction pour les prochaines parties";
        AudioSource.PlayClipAtPoint(oneShotClip, transform.position);
        endGameCanvas.SetActive(true);
    }

    // TODO: Changer ceci car les sons vont aussi s'arrêter
    private void StopGameLoop()
    {
        Time.timeScale = CurrentGameState == GameState.Running ? 1 : 0;
    }
}
