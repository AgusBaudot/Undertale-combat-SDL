using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static /*see for synchronized for multi-threading*/ void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new GameManager();
            }
        }

        public GameState GetGameState() => currentState;

        public void OnGameStateChanged(GameState newState)
        {
            Engine.Debug(newState.ToString());
            if ((currentState == GameState.Win || currentState == GameState.Lose) && newState == GameState.MainMenu)
            {
                Engine.Debug("Level reset");    
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
