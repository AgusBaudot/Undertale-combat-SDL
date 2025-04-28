using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class MainMenu
    {
        private Image bg;
        private PlayButton playBtn;
        private QuitButton quitBtn;

        public MainMenu()
        {
            bg = Engine.LoadImage("assets/fondo.png");
            playBtn = new PlayButton(360, 600);
            quitBtn = new QuitButton(720, 600);
        }

        public void Update()
        {
            playBtn.Update();
            quitBtn.Update();
        }

        public void Render()
        {
            playBtn.Render();
            quitBtn.Render();
        }
    }
}
