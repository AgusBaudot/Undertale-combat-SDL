using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class KeyboardHandler
    {
        private Dictionary<string, bool> keyboardState;

        public KeyboardHandler()
        {
            keyboardState = new Dictionary<string, bool>
            {
                {"W", false},
                {"S", false},
                {"A", false},
                {"D", false}
            };
        }

        public void SetKeyboard(string key, bool isPressed)
        {
            keyboardState[key] = isPressed;
        }

        public Dictionary<string, bool> GetKeyboardState() => keyboardState;
    }
}
