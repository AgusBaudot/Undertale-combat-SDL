using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player
    {
        private Transform transform;
        private PlayerController playerController;
        private SpriteRenderer renderer;
        private Image playerSprite = Engine.LoadImage("assets/player.png");

        public Player(float positionX, float positionY)
        {
            transform = new Transform(positionX, positionY);
            playerController = new PlayerController(transform, playerSprite.width, playerSprite.height);
            renderer = new SpriteRenderer(transform, playerSprite);
        }

        public Player(float posX, float posY, float width, float height)
        {
            transform = new Transform(posX, posY, width, height);
            playerController = new PlayerController(transform, playerSprite.width, playerSprite.height);
            renderer = new SpriteRenderer(transform, playerSprite);
        }

        public void Update()
        {
            playerController.Inputs();
            //logic update.
        }

        public void Render()
        {
            renderer.Render();
        }

    }
}
