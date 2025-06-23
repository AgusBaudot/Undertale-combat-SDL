using Tao.Sdl;

namespace MyGame
{
    public class QuitButton : IButton
    {
        private Transform transform;
        private bool leftButton = true;
        private string normalSprite, selectedSprite;
        private SpriteRenderer renderer;

        public QuitButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite = "assets/Sprites/ExitButton.png";
            selectedSprite = "assets/Sprites/ExitButtonPressed.png";
            renderer = new SpriteRenderer(transform, Engine.LoadImage(normalSprite));
        }

        public void Update()
        {
            Inputs();

            if (!leftButton && Engine.GetKeyDown(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        public void Inputs()
        {
            if (Engine.GetKeyDown(Engine.KEY_A))
            {
                leftButton = !leftButton;
            }

            else if (Engine.GetKeyDown(Engine.KEY_D))
            {
                leftButton = !leftButton;
            }
        }

        public void Pressed()
        {
            SdlMixer.Mix_CloseAudio();
            Sdl.SDL_Quit();
        }

        public void Render()
        {
            renderer.UpdateSprite((!leftButton) ? selectedSprite : normalSprite);
            renderer.Render();
        }
    }
}
