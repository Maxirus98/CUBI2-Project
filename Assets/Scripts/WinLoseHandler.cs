using TMPro;
using UnityEngine;

public enum GameState
{
    Running,
    Won,
    Lost
}

/// <summary>
/// Script permettant de d�terminer si les joueurs ont gagn�s ou perdus
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
            case GameState.Won:
                EndGame("Partie gagn�e", SoundManager.Instance.winSound);
                break;
            case GameState.Lost:
                EndGame("Partie perdue", SoundManager.Instance.lostSound);
                break;
            default:
                break;
        }

        // Arr�te le jeu et tous les �v�nements apr�s 3 secondes
        Invoke(nameof(StopGameLoop), 3f);
    }

    private void EndGame(string text, AudioClip oneShotClip)
    {
        endGameText.text = text;
        AudioSource.PlayClipAtPoint(oneShotClip, transform.position);
        endGameCanvas.SetActive(true);
    }

    // TODO: Changer ceci car les sons vont aussi s'arr�ter
    private void StopGameLoop()
    {
        Time.timeScale = CurrentGameState == GameState.Running ? 1 : 0;
    }
}
