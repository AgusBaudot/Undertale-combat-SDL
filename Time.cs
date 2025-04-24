using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Time
    {
        static public float deltaTime { get; private set; }
        private float timeLastFrame;
        private DateTime initialTime;
        private float fixedDeltatime = 0.02f;

        public Time()
        {
            initialTime = DateTime.Now;
        }

        public void UpdateTime()
        {
            float currentTime = (float)(DateTime.Now - initialTime).TotalSeconds;
            deltaTime = currentTime - timeLastFrame;
            timeLastFrame = currentTime;
        }
    }
}
