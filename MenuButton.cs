using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class MenuButton : IButton
    {
        private Transform transform;
        private bool leftButton = true;
        private string normalSprite, selectedSprite;
        private SpriteRenderer renderer;
        private GameManager instance;

        public MenuButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite = "assets/Sprites/MenuButton.png";
            selectedSprite = "assets/Sprites/MenuButtonPressed.png";
            renderer = new SpriteRenderer(transform, Engine.LoadImage(normalSprite));
            instance = GameManager.GetInstance();
        }

        public void Update()
        {
            Inputs();

            if (leftButton && Engine.GetKeyDown(Engine.KEY_ESP))
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
            instance.OnGameStateChanged(GameState.MainMenu);
        }

        public void Render()
        {
            renderer.UpdateSprite((leftButton) ? selectedSprite : normalSprite);
            renderer.Render();
        }
    }
}
