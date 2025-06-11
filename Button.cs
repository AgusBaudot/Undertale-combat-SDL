using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface IButton
    {

        public void Update();

        public void Inputs();

        public void Pressed();

        public void Render();
    }
}
