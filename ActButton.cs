using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ActButton : Button
    {
        public ActButton(float x, float y)
        {
            transform = new Transform(x, y);
            transform = new Transform(x, y);
            normalSprite[1] = Engine.LoadImage("assets/player.png");
            selectedSprite[1] = Engine.LoadImage("assets/player.png");
        }

        public void update()
        {
            Update();
            if (buttons[0] == true)
            {
                Pressed();
            }
        }

        public void hide()
        {
            hide();
        }

        protected override void Pressed()
        {

        }
    }
}
