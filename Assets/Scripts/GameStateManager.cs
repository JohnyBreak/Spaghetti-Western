using System;

public static class GameStateManager
{
    public enum GameState 
    {
        None = 0,
        Paused = 1,
        GamePlay = 2,
        GameOver = 3,
    }

    public static GameState CurrentGameState { get; private set; }
    public static event Action<GameState> GameStateChangedEvent;

    public static void SetState(GameState newState) 
    {
        if (newState == CurrentGameState) return;

        CurrentGameState = newState;
        GameStateChangedEvent?.Invoke(newState);
    }
}
