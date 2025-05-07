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
        public static IntPtr music;

        static void Main(string[] args)
        {
            Engine.Initialize(1080, 720);

            level = new LevelManager();
            time = new Time();
            mainMenu = new MainMenu();
            win = new WinScreen();
            lose = new LoseScreen();
            instance = GameManager.GetInstance();

            instance.OnLevelReset += ResetLevel;


            music = SdlMixer.Mix_LoadMUS("assets/Music/DeathByGlamour.wav");
            //sfx= SdlMixer.Mix_LoadWAV(); //for sound efects.
            SdlMixer.Mix_PlayMusic(music, -1);
            //SdlMixer.Mix_PlayChannel(int channel, sfx, -1 for loop & 0? for one time track); for playing sound effects.
            SdlMixer.Mix_VolumeMusic(128);
            //if (music == IntPtr.Zero)
            //{
            //    Engine.Debug($"Music failed to load: {Sdl.SDL_GetError()}");
            //}
            //else
            //{
            //    SdlMixer.Mix_PlayMusic(music, -1);
            //    Engine.Debug((SdlMixer.Mix_PlayingMusic()).ToString());
            //} //Check if it's imported right.

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

        static void ResetLevel()
        {
            level.ResetLevel();
        }
    }
}