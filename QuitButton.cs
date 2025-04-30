using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public class QuitButton : Button
    {
        private string normalSprite, selectedSprite;
        private SpriteRenderer renderer;

        public QuitButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite = "assets/ExitButton.png";
            selectedSprite = "assets/ExitButtonPressed.png";
            renderer = new SpriteRenderer(transform, Engine.LoadImage(normalSprite));
        }

        public override void Update()
        {
            base.Update();

            if (!leftButton && Engine.GetKeyDown(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        private void Pressed()
        {
            Sdl.SDL_Quit();
        }

        public void Render()
        {
            renderer.UpdateSprite((!leftButton) ? selectedSprite : normalSprite);
            renderer.Render();
        }
    }
}
