using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AttackButton : Button //See ActButton class for logic.
    {
        #region Classes
        private SpriteRenderer spriteRenderer;
        private HealthController enemyHealth;
        private GameManager instance;
        #endregion
        #region Internal variables
        private string normalSpritePath, selectedSpritePath;
        #endregion

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

            if (leftButton && Engine.GetKeyDown(Engine.KEY_ESP)) //To see if this button is pressed, check if leftButton is true instead of false.
            {
                Pressed();
            }
        }

        private void Pressed()
        {
            enemyHealth.TakeDamage(10); //Instead of healing the player, the attack button will do damage to the enemy.
            instance.OnGameStateChanged(GameState.EnemyTurn);
        }

        public void Render()
        {
            spriteRenderer.UpdateSprite((leftButton) ? selectedSpritePath : normalSpritePath); //Now selected sprite will render if leftButton is true.
            spriteRenderer.Render();
        }
    }
}
