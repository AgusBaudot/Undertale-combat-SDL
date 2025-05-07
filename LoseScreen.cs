using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LoseScreen
    {
        private Image bg;
        private Transform transform;
        private SpriteRenderer spriteRenderer;
        private MenuButton menuButton;
        private QuitButton quitBtn;

        public LoseScreen()
        {
            bg = Engine.LoadImage("assets/Sprites/lose.png");
            menuButton = new MenuButton(360, 600);
            quitBtn = new QuitButton(720, 600);

            transform = new Transform(Engine.center);
            spriteRenderer = new SpriteRenderer(transform, bg);
        }

        public void Update()
        {
            menuButton.Update();
            quitBtn.Update();
        }

        public void Render()
        {
            spriteRenderer.Render();
            menuButton.Render();
            quitBtn.Render();
        }
    }
}

