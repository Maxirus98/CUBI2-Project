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
/// Script permettant de dï¿½terminer si les joueurs ont gagnï¿½s ou perdus
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
                EndGame("Attention! Les cauchemars ont rï¿½ussi leur objectif. La petite Rosalie se rï¿½veille de son cauchemar.", SoundManager.Instance.lostSound);
                break;
            case GameState.Won:
                EndGame("Bravo! Le rï¿½ve a ï¿½tï¿½ stabilisï¿½, vous avez vaincu tous les cauchemars", SoundManager.Instance.winSound);
                break;
            case GameState.HpLost:
                EndGame("Attention! Un des dï¿½fenseurs oniriques est tombï¿½. La petite Sophie se rï¿½veille de son cauchemar.", SoundManager.Instance.lostSound);
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

        endGameText.text = text + $" Vous avez construit {turretBuiltCount} tours. Vous n'aurez pas accès à ces emplacements de construction pour les prochaines parties";
        AudioSource.PlayClipAtPoint(oneShotClip, transform.position);
        endGameCanvas.SetActive(true);
    }

    // TODO: Changer ceci car les sons vont aussi s'arrï¿½ter
    private void StopGameLoop()
    {
        Time.timeScale = CurrentGameState == GameState.Running ? 1 : 0;
    }
}
