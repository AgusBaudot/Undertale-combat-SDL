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
        static private Time time;
        static private GameManager instance;
        static private LevelManager level;
        static private MainMenu mainMenu;

        static void Main(string[] args)
        {
            Engine.Initialize(1080, 720);

            level = new LevelManager();
            time = new Time();
            mainMenu = new MainMenu();
            instance = GameManager.GetInstance();
            

            while (true)
            {
                time.UpdateTime();
                Engine.UpdateInput(); //Update input.

                /*if (Engine.GetKeyDown(Engine.KEY_P))
                {
                    instance.OnGameStateChanged((instance.GetGameState() == (GameState)2) ? (GameState)3 : (GameState)2); //Toggle between gamestate 2 & 3
                }*/

                Update();
                Render();
            }
        }

        static void Update()
        {
            switch (instance.GetGameState())
            {
                case GameState.MainMenu:
                    mainMenu.Update();
                    break;
                case GameState.EnterBattle:
                    break;
                case GameState.EnemyTurn:
                    level.Update(); //Level already manages difference between enemy and player turn.
                    break;
                case GameState.PlayerTurn:
                    level.Update(); //Level already manages difference between enemy and player turn.
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
            }
        }
        
        static void Render()
        {
            Engine.Clear();
            switch (instance.GetGameState())
            {
                case GameState.MainMenu:
                    mainMenu.Render();
                    break;
                case GameState.EnterBattle:
                    break;
                case GameState.EnemyTurn:
                    level.Render(); //Level already manages difference between enemy and player turn.
                    break;
                case GameState.PlayerTurn:
                    level.Render(); //Level already manager difference between enemy and player turn.
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
            }
            Engine.Show();
        }
    }
}