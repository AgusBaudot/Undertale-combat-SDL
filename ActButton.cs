using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ActButton : Button
    {
        private int heal = 20;
        private string normalSpritePath, selectedSpritePath;
        private SpriteRenderer spriteRenderer;
        private HealthController playerHealth;
        private GameManager instance;

        public ActButton(float x, float y, HealthController playerHealth)
        {
            transform = new Transform(x, y);
            normalSpritePath = "assets/ActButton.png";
            selectedSpritePath = "assets/ActButtonPressed.png";
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage(normalSpritePath));
            this.playerHealth = playerHealth;
            instance = GameManager.GetInstance();
        }

        public override void Update()
        {
            base.Update();

            if (!attackButton && Engine.GetKeyDown(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        private void Pressed()
        {
            playerHealth.Recover(heal);
            instance.OnGameStateChanged(GameState.EnemyTurn);
        }

        public void Render()
        {
            spriteRenderer.UpdateSprite((!attackButton) ? selectedSpritePath : normalSpritePath);
            spriteRenderer.Render();
        }
    }
}
