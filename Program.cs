using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net.NetworkInformation;
using Tao.Sdl;



namespace MyGame
{

    class Program
    {
        #region Level
        static private Image fondo = Engine.LoadImage("assets/fondo.png");
        static private Player player1;
        static private CombatArea combatArea;
        #endregion
        #region UI
        static private AttackButton attackButton;
        static private ActButton actButton;
        #endregion
        #region Enemy
        static private Enemy enemy;
        #endregion
        static private Time time;
        static private GameManager instance;

        static void Main(string[] args)
        {
            Engine.Initialize(1080, 720);

            player1 = new Player(Engine.center);
            enemy = new Enemy(player1, 560, 90);
            combatArea = new CombatArea();
            instance = GameManager.GetInstance();
            attackButton = new AttackButton(360, 600, enemy.healthController);
            actButton = new ActButton(720, 600, player1.healthController);
           
            time = new Time();
            

            while (true)
            {
                time.UpdateTime();
                if (Engine.GetKey(Engine.KEY_P))
                {
                    instance.OnGameStateChanged((instance.GetGameState() == (GameState)2) ? (GameState)3 : (GameState)2); //Toggle between gamestate 2 & 3
                }

                Update();
                Render();
            }

        }

        static void Update()
        {
            enemy.Update();

            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                player1.Update();
            }
            if (instance.GetGameState() == GameState.PlayerTurn)
            {
                attackButton.Update();
                actButton.Update();
            }

        }
        
        static void Render()
        {
            Engine.Clear();
            Engine.Draw(fondo, 0, 0);
            combatArea.Render();
            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                player1.Render();
            }
            enemy.Render();

            attackButton.Render();
            actButton.Render();
            
            Engine.Show();
        }
    }
}