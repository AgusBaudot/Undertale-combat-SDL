using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public struct Vector2
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float magnitude { get; private set; }
        public Vector2 normalized => (magnitude == 0) ? zero : new Vector2(x /= magnitude, y /= magnitude);
        public static Vector2 up = new Vector2(0, -1);
        public static Vector2 down = new Vector2(0, 1);
        public static Vector2 right = new Vector2(1, 0);
        public static Vector2 left = new Vector2(-1, 0);
        public static Vector2 zero = new Vector2(0, 0);
        public static Vector2 one = new Vector2(1, 1);

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
            magnitude = (float)Math.Sqrt((x * x) + (y * y));
        }

        public static Vector2 operator *(Vector2 vector, float number)
        {
            return new Vector2(vector.x * number, vector.y * number);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).magnitude;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public string ToString(string format)
        {
            return $"({x.ToString(format)}, {y.ToString(format)})";
        }

        public Vector2 Normalize()
        {
            return new Vector2(x /= magnitude, y /= magnitude);
        }

    }
}
