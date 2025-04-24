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


        public AttackButton(float x, float y)
        {
            transform = new Transform(x, y);
            _normalSpritePath = "assets/ActButton.png"; //Hacer otro
            _selectedSpritePath = "assets/ActButtonPressed.png"; //Hacer otro
            _spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage(_normalSpritePath));
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
            Engine.Debug("Attack");
        }

        public void Render()
        {
            _spriteRenderer.UpdateSprite((attackButton) ? _selectedSpritePath : _normalSpritePath);
            _spriteRenderer.Render();
        }
    }
}
