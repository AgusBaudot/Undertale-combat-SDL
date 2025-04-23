using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.assets;

namespace MyGame
{
    public class Player
    {
        private Transform transform;
        private PlayerController playerController;
        private SpriteRenderer renderer;
        private BoxCollider collider;
        private Image playerSprite = Engine.LoadImage("assets/player.png");

        public Player(Vector2 position)
        {
            transform = new Transform(position);
            playerController = new PlayerController(transform, playerSprite.width, playerSprite.height);
            renderer = new SpriteRenderer(transform, playerSprite);
            collider = new BoxCollider(transform, renderer);
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
            collider.Update();
            //logic update.
        }

        public void Render()
        {
            renderer.Render();
        }

        public BoxCollider GetCollider() => collider;

    }
}
