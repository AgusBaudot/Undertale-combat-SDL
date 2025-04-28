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
        protected bool attackButton = true;

        public virtual void Update()
        {
            Inputs();
        }

        public void Inputs()
        {

            if (Engine.GetKeyDown(Engine.KEY_A))
            {
                attackButton = !attackButton;
            }

            else if (Engine.GetKeyDown(Engine.KEY_D))
            {
                attackButton = !attackButton;
            }
        }
    }
}
