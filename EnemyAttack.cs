using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.assets;

namespace MyGame
{
    public class EnemyAttack
    {
        private Vector2 speed;
        private int damage = 10;
        private HealthController playerHealth;
        public Transform transform {  get; private set; }   
        private SpriteRenderer spriteRenderer;
        private BoxCollider collider, playerCollider;
        private Enemy fatherEnemy;

        public EnemyAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, Enemy enemy)
        {
            this.speed = speed;
            transform = new Transform(position);
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage("assets/Sprites/attack.png"));
            collider = new BoxCollider(transform, spriteRenderer);
            this.playerCollider = playerCollider;
            this.playerHealth = playerHealth;
            fatherEnemy = enemy;
        }

        public void Update()
        {
            transform.Translate(speed);
            collider.Update();
            CheckCollisions();
        }

        public void UpdateSpeed (Vector2 newSpeed)
        {
            speed = newSpeed;
        }

        public void Render()
        {
            spriteRenderer.Render();
        }

        private void CheckCollisions()
        {
            if (Math.Abs(transform.position.x - playerCollider.center.x) < (spriteRenderer.scaledWidth/2 + Math.Abs(playerCollider.center.x - playerCollider.max.x)) &&
                Math.Abs(transform.position.y - playerCollider.center.y) < (spriteRenderer.scaledHeight/2 + Math.Abs(playerCollider.center.y - playerCollider.max.y)))
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
