using System;

namespace MyGame
{
    public class GameManager
    {
        private static GameManager Instance;
        private GameState currentState = 0;
        public event Action OnLevelReset;

        public static GameManager GetInstance()
        {
            if (Instance == null)
            {
                CreateInstance();
            }
            return Instance;
        }

        private static void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new GameManager();
            }
        }

        public GameState GetGameState() => currentState;

        public void OnGameStateChanged(GameState newState)
        {
            if ((currentState == GameState.Win || currentState == GameState.Lose) && newState == GameState.MainMenu)
            {
                OnLevelReset?.Invoke();
            }
            currentState = newState;
        }
    }

    public enum GameState
    {
        MainMenu,
        EnterBattle,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    };
}
