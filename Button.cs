using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Button
    {
        protected Transform transform;  
        protected Image[] normalSprite = new Image[2];
        protected Image[] selectedSprite = new Image[2];
        private SpriteRenderer normalRenderer;
        private SpriteRenderer selectedRenderer;

        protected bool[] buttons = new bool[2];
        private int currentButton = 0;

        public void Update()
        {
            Inputs();
        }

        public void Render()
        {
            if (buttons[0] == true) 
            {
                selectedRenderer = new SpriteRenderer(transform, selectedSprite[0]);
                normalRenderer = new SpriteRenderer(transform, normalSprite[1]);
            }
            if (buttons[1] == true)
            {
                selectedRenderer = new SpriteRenderer(transform, selectedSprite[1]);
                normalRenderer = new SpriteRenderer(transform, normalSprite[2]);
            }

            normalRenderer.Render();
            selectedRenderer.Render();
        }

        public void Inputs()
        {

            if (Engine.GetKey(Engine.KEY_A))
            {
                buttons[currentButton] = false;

                if (currentButton == 0)
                {
                    currentButton = 1;
                    buttons[currentButton] = true;
                    Engine.Debug(currentButton.ToString());
                    return;
                }
                currentButton--;
                buttons[currentButton] = true;

                Engine.Debug(currentButton.ToString());
            }

            if (Engine.GetKey(Engine.KEY_D))
            {
                buttons[currentButton] = false;

                if (currentButton == 1)
                {
                    currentButton = 0;
                    buttons[currentButton] = true;
                    Engine.Debug(currentButton.ToString());
                    return;
                }
                currentButton++;
                buttons[currentButton] = true;

                Engine.Debug(currentButton.ToString());
            }
        }

        public void Hide()
        {

        }

        protected virtual void Pressed()
        {

        }
    }
}
