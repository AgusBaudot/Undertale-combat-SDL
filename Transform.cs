using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Transform
    {
        public Vector2 position { get; /*private*/ set; }
        public Vector2 scale { get; private set; }

        public Transform(float x, float y)
        {
            position = new Vector2(x, y);
            scale = Vector2.one;
        }

        public Transform(float x, float y, float w, float h)
        {
            position = new Vector2(x, y);
            scale = new Vector2(w, h);
        }

        public void Translate(Vector2 translation)
        {
            position += translation;
        }

    }
}
