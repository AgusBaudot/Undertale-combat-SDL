using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class CombatArea
    {
        public Vector2 position;
        public Vector2 scale {  get; private set; }
        public Transform transform {  get; private set; }
        public Transform transform2 { get; private set; }
        private SpriteRenderer renderer1;
        private SpriteRenderer renderer2;
        private Image boxSprite = Engine.LoadImage("assets/combatArea.png");
        private Image edgesSprite = Engine.LoadImage("assets/combatAreaBack.png");

        public CombatArea()
        {
            position = new Vector2(1080/2, 720/2);
            transform = new Transform(position.x, position.y);
            transform2 = new Transform(position.x, position.y);
            renderer1 = new SpriteRenderer (transform, boxSprite);
            renderer2 = new SpriteRenderer(transform2, edgesSprite);
            scale = new Vector2(boxSprite.width, boxSprite.height);
        }

        public void Render()
        {
            renderer1.Render();
            renderer2.Render();
        }
    }

    
}
