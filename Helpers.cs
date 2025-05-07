using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Helpers
    {
        /// <summary>
        /// Clamps the value between min and max.
        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
        /// </summary>

        /// Linearly interpolates between a nd b by t (0 to 1).
        /// <summary>
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(t, 0f, 1f);
        }
        /// </summary>

        /// <summary>
        /// Remaps a value from one range to another.
        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            if (Math.Abs(fromMax - fromMin) < float.Epsilon) return toMin;
            float normalized = (value - fromMin) / (fromMax - fromMin);
            return Lerp(toMin, toMax, normalized);
        }
        /// </summary>

        /// <summary>
        /// Shuffles a list in place.
        public static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
        /// </summary>

        /// <summaty>
        /// Wraps a value around a range.
        public static float Wrap(float value, float min, float max)
        {
            float range = max - min;
            if (range == 0f) return min;
            return (value - min) % range + min;
        }
        /// </summaty>
    }
}
