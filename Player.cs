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
        private int health = 100;
        private int maxHealth = 100;
        private float invencibilityDuration = 2;

        private Transform transform;
        private PlayerController playerController;
        private SpriteRenderer renderer;
        private BoxCollider collider;
        public HealthController healthController;
        private Image playerSprite = Engine.LoadImage("assets/player.png");

        private GameManager instance;

        public Player(Vector2 position)
        {
            transform = new Transform(position);
            playerController = new PlayerController(transform, playerSprite.width, playerSprite.height);
            renderer = new SpriteRenderer(transform, playerSprite);
            collider = new BoxCollider(transform, renderer);
            healthController = new HealthController(health, maxHealth, invencibilityDuration);
            instance = GameManager.GetInstance();
        }

        public Player(float posX, float posY, float width, float height)
        {
            transform = new Transform(posX, posY, width, height);
            playerController = new PlayerController(transform, playerSprite.width, playerSprite.height);
            renderer = new SpriteRenderer(transform, playerSprite);
        }

        public void Update()
        {
            healthController.Update();
            playerController.Inputs();
            IsAlive();
        }

        public void FixedUpdate()
        {
            playerController.Move();
            collider.Update();
        }

        public void Render()
        {
            renderer.Render();
        }

        private void IsAlive()
        {
            if (healthController.health <= 0)
            {
                instance.OnGameStateChanged(GameState.Lose);
            }
        }

        public BoxCollider GetCollider() => collider;

    }
}
