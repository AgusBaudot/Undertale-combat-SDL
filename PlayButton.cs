using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayButton : Button
    {
        private string normalSprite, selectedSprite;
        private SpriteRenderer renderer;
        private GameManager instance;

        public PlayButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite = "assets/StartButton.png";
            selectedSprite = "assets/StartButtonPressed.png";
            renderer = new SpriteRenderer(transform, Engine.LoadImage(normalSprite));
            instance = GameManager.GetInstance();
        }

        public override void Update()
        {
            base.Update();

            if (attackButton && Engine.GetKeyDown(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        private void Pressed()
        {
            instance.OnGameStateChanged(GameState.PlayerTurn);
        }

        public void Render()
        {
            renderer.UpdateSprite((attackButton) ? selectedSprite : normalSprite);
            renderer.Render();
        }
    }
}
