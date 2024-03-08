using TMPro;
using Unity.Netcode;
using UnityEngine;

public enum GameState
{
    Running,
    Won,
    Lost
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
    [SerializeField]
    private AudioSource gameAudioSource;
    [SerializeField]
    private AudioClip gameLostAudio;
    [SerializeField]
    private AudioClip gameWonAudio;

    public GameState CurrentGameState {get; set;}

    public void UpdateGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
        switch (CurrentGameState)
        {
            case GameState.Running:
                
                break;
            case GameState.Won:
                EndGame("Partie gagnée!", gameWonAudio);
                break;
            case GameState.Lost:
                EndGame("Partie perdue", gameLostAudio);
                break;
            default:
                break;
        }

        Time.timeScale = CurrentGameState == GameState.Running ? 1 : 0;
    }

    private void EndGame(string text, AudioClip oneShotClip)
    {
        endGameText.text = text;
        //gameAudioSource.PlayOneShot(oneShotClip);
        endGameCanvas.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        RelayConnectionManager.Instance.DisconnectFromServer();
    }
}
