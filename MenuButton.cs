using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class MenuButton : Button
    {
        private string normalSprite, selectedSprite;
        private SpriteRenderer renderer;
        private GameManager instance;

        public MenuButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite = "assets/MenuButton.png";
            selectedSprite = "assets/MenuButtonPressed.png";
            renderer = new SpriteRenderer(transform, Engine.LoadImage(normalSprite));
            instance = GameManager.GetInstance();
        }

        public override void Update()
        {
            base.Update();

            if (leftButton && Engine.GetKeyDown(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        private void Pressed()
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
