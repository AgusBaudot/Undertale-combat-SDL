using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net.NetworkInformation;
using Tao.Sdl;



namespace MyGame
{

    class Program
    {
        
        static private Image fondo = Engine.LoadImage("assets/fondo.png");
        static private Player player1;
        static private int width, height;

        static void Main(string[] args)
        {
            width = 1080;
            height = 720;
            Engine.Initialize(width, height);
            player1 = new Player(width/2, height/2);

            while (true)
            {
                Update();
                Render();
            }

        }

        static void Update()
        {
            player1.Update();
        }
        
        static void Render()
        {
            Engine.Clear();
            Engine.Draw(fondo, 0, 0);
            player1.Render();
            Engine.Show();
        }
    }
}