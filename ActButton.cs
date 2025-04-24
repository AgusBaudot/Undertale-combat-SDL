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
        private string _normalSpritePath, _selectedSpritePath;
        private SpriteRenderer _spriteRenderer;
        private HealthController playerHealth;

        public ActButton(float x, float y, HealthController playerHealth)
        {
            transform = new Transform(x, y);
            _normalSpritePath = "assets/ActButton.png";
            _selectedSpritePath = "assets/ActButtonPressed.png";
            _spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage(_normalSpritePath));
            this.playerHealth = playerHealth;
        }

        public override void Update()
        {
            base.Update();

            if (!attackButton && Engine.GetKey(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        private void Pressed()
        {
            playerHealth.Recover(heal);
        }

        public void Render()
        {
            _spriteRenderer.UpdateSprite((!attackButton) ? _selectedSpritePath : _normalSpritePath);
            _spriteRenderer.Render();
        }
    }
}
