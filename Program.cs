using System;
using Tao.Sdl;

namespace MyGame
{
    class Program
    {
        static private Time time;
        static private GameManager instance;
        static private LevelManager level;
        static private MainMenu mainMenu;
        static private WinScreen win;
        static private LoseScreen lose;
        static private Music music;
        static private Font normalFont;

        static void Main(string[] args)
        {
            Engine.Initialize(1080, 720);

            level = new LevelManager();
            time = new Time();
            mainMenu = new MainMenu();
            win = new WinScreen();
            lose = new LoseScreen();
            music = new Music();
            instance = GameManager.GetInstance();
            normalFont = new Font("assets/Fonts/UndertaleFont.ttf", 24);

            instance.OnLevelReset += ResetLevel;

            while (true)
            {
                time.UpdateTime();
                Engine.UpdateInput(); //Update input.

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
                    level.SetPosition();
                    level.Update(); //Level already manages difference between enemy and player turn.
                    break;
                case GameState.Win:
                    win.Update();
                    break;
                case GameState.Lose:
                    lose.Update();
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
                    win.Render();
                    break;
                case GameState.Lose:
                    lose.Render();
                    break;
            }
            Engine.Show();
        }

        static public Font GetFont() => normalFont;

        static private void ResetLevel()
        {
            level.ResetLevel();
        }
    }
}