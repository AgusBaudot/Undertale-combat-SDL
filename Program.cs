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
        #region Time
        static private float deltaTime;
        static private float timeLastFrame;
        static private DateTime initialTime;
        static private float fixedDeltatime = 0.02f;
        static public float DeltaTime => deltaTime;
        #endregion
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
        static private EnemyAttack attacktest;
        #endregion

        static private GameManager instance;

        static void Main(string[] args)
        {
            Engine.Initialize(1080, 720);
            player1 = new Player(Engine.center);
            combatArea = new CombatArea();
            instance = GameManager.GetInstance();
            attackButton = new AttackButton(360, 600);
            actButton = new ActButton(720, 600);
            initialTime = DateTime.Now;
            

            while (true)
            {
                float currentTime = (float)(DateTime.Now - initialTime).TotalSeconds;
                deltaTime = currentTime - timeLastFrame;
                timeLastFrame = currentTime;
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
            if (attacktest == null)
            {
                attacktest = new EnemyAttack(new Vector2(0, Engine.center.y), Vector2.right * 5, player1.GetCollider());
            }
            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                player1.Update();
                attacktest.Update();
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
            attackButton.Render();
            actButton.Render();
            attacktest.Render();
            Engine.Show();
        }
    }
}