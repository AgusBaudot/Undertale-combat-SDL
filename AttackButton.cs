using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AttackButton : Button
    {
        private string _normalSpritePath, _selectedSpritePath;
        private SpriteRenderer _spriteRenderer;
        private HealthController enemyHealth;
        private int damage = 10;

        public AttackButton(float x, float y, HealthController enemyHealth)
        {
            transform = new Transform(x, y);
            _normalSpritePath = "assets/AtkButton.png"; //Hacer otro
            _selectedSpritePath = "assets/AtkButtonPressed.png"; //Hacer otro
            _spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage(_normalSpritePath));
            this.enemyHealth = enemyHealth;
        }

        public override void Update()
        {
            base.Update();

            if (attackButton && Engine.GetKey(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        private void Pressed()
        {
            enemyHealth.TakeDamage(10);
        }

        public void Render()
        {
            _spriteRenderer.UpdateSprite((attackButton) ? _selectedSpritePath : _normalSpritePath);
            _spriteRenderer.Render();
        }
    }
}
