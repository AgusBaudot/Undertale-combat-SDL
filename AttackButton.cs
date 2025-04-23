using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AttackButton : Button 
    {
        public AttackButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite[0] = Engine.LoadImage("assets/player.png");
            selectedSprite[0] = Engine.LoadImage("assets/player.png");
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

        }

        protected override void Pressed()
        {
            
        }
    }
}
