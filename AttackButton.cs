using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AttackButton : Button
    {
        private string normalSpritePath, selectedSpritePath;
        private SpriteRenderer spriteRenderer;
        private HealthController enemyHealth;
        private GameManager instance;

        public AttackButton(float x, float y, HealthController enemyHealth)
        {
            transform = new Transform(x, y);
            normalSpritePath = "assets/AtkButton.png";
            selectedSpritePath = "assets/AtkButtonPressed.png";
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage(normalSpritePath));
            this.enemyHealth = enemyHealth;
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
            enemyHealth.TakeDamage(10);
            instance.OnGameStateChanged(GameState.EnemyTurn);
        }

        public void Render()
        {
            spriteRenderer.UpdateSprite((attackButton) ? selectedSpritePath : normalSpritePath);
            spriteRenderer.Render();
        }
    }
}
