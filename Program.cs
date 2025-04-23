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
        static private GameManager instance;
        static private Image fondo = Engine.LoadImage("assets/fondo.png");
        static private Player player1;
        static private CombatArea combatArea;
        static private int width, height;
        static private AttackButton attackButton;
        static private ActButton actButton;

        static void Main(string[] args)
        {
            width = 1080;
            height = 720;
            Engine.Initialize(width, height);
            player1 = new Player(width/2, height/2);
            combatArea = new CombatArea();
            instance = GameManager.GetInstance();
            attackButton = new AttackButton(440, 600);
            actButton = new ActButton(440, 600);
            

            while (true)
            {

                if (Engine.GetKey(Engine.KEY_I))
                {
                    instance.OnGameStateChanged((GameState)2);
                }
                if (Engine.GetKey(Engine.KEY_U))
                {
                    instance.OnGameStateChanged((GameState)3);
                }

                Update();
                Render();
            }

        }

        static void Update()
        {
            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                player1.Update();
            }
            if (instance.GetGameState() == GameState.PlayerTurn)
            {
                
                attackButton.update();
                actButton.update();
            }

        }
        
        static void Render()
        {
            Engine.Clear();
            Engine.Draw(fondo, 0, 0);

            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                combatArea.Render();
                player1.Render();
            }
            if (instance.GetGameState() == GameState.PlayerTurn)
            {
                //attackButton.Render();
                //actButton.Render();
            }
            Engine.Show();
        }
    }
}