using System;

namespace MyGame
{
    public class Time
    {
        static public float deltaTime { get; private set; }
        private float timeLastFrame;
        private DateTime initialTime;

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
