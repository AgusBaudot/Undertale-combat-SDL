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
        /// Clamps a value around a range.
        /// </summary>
        /// <param valuetest="value"></param>
        /// <param mintest="min"></param>
        /// <param maxtest="max"></param>
        /// <returns></returns>
        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        /// <summary>
        /// Linearly interpolates between a and b by t (clamped from 0 to 1).
        /// </summary>
        /// <param start="a"></param>
        /// <param end="b"></param>
        /// <param t="t"></param>
        /// <returns></returns>
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(t, 0f, 1f);
        }

        /// <summary>
        /// Remaps a value from one range to another.
        /// </summary>
        /// <param value="value"></param>
        /// <param old min value="fromMin"></param>
        /// <param old max value="fromMax"></param>
        /// <param new min value="toMin"></param>
        /// <param new max value="toMax"></param>
        /// <returns></returns>
        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            if (Math.Abs(fromMax - fromMin) < float.Epsilon) return toMin;
            float normalized = (value - fromMin) / (fromMax - fromMin);
            return Lerp(toMin, toMax, normalized);
        }

        /// <summary>
        /// Shuffles a List in place.
        /// </summary>
        /// <param list="list"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Wraps a value around a range.
        /// </summary>
        public static float Wrap(float value, float min, float max)
        {
            float range = max - min;
            if (range == 0f) return min;
            return (value - min) % range + min;
        }
    }
}
