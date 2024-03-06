using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script permettant de déterminer si les joueurs ont gagnés ou perdus
/// </summary>
public class WinLoseHandler : MonoBehaviour
{
    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }
    public enum GameState
    {
        Running,
        Won,
        Lost
    }
    public EventGameState gameStateHandler;
    public GameState CurrentGameState { get; set; }

    void UpdateGameState(GameState newGameState)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = newGameState;
        switch (CurrentGameState)
        {
            case GameState.Running:
                break;
            default:
                break;
        }

        gameStateHandler.Invoke(CurrentGameState, previousGameState);
    }
}
