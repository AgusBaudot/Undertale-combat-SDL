using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using MyGame.assets;

namespace MyGame
{

    public abstract class BaseBoneAttack
    {
        public Vector2 speed { get; private protected set; }
        private protected int damage = 10;
        private protected HealthController playerHealth;
        public Transform transform { get; private protected set; }
        private protected SpriteRenderer spriteRenderer;

        private protected BoxCollider collider, playerCollider;
        private protected PlayerController playerController;

        public void Update()
        {
            transform.Translate(speed);
            collider.Update();
            CheckCollisions();
        }

        public void UpdateSpeed(Vector2 newSpeed)
        {
            speed = newSpeed;
        }
        public void Render()
        {
            spriteRenderer.Render();
        }

        public virtual void CheckCollisions()
        {
            if (Math.Abs(transform.position.x - playerCollider.center.x) < (spriteRenderer.scaledWidth / 2 + Math.Abs(playerCollider.center.x - playerCollider.max.x)) &&
                Math.Abs(transform.position.y - playerCollider.center.y) < (spriteRenderer.scaledHeight / 2 + Math.Abs(playerCollider.center.y - playerCollider.max.y)))
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public class WhiteBoneAttack : BaseBoneAttack
    {
        public WhiteBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, Enemy enemy)
        {
            base.speed = speed;
            transform = new Transform(position);
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage("assets/Sprites/White bone attack.png"));
            collider = new BoxCollider(transform, spriteRenderer);
            base.playerCollider = playerCollider;
            base.playerHealth = playerHealth;
        }
    }

    public class BlueBoneAttack : BaseBoneAttack
    {
        public BlueBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            base.speed = speed;
            transform = new Transform(position);
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage("assets/Sprites/Blue bone attack.png"));
            collider = new BoxCollider(transform, spriteRenderer);
            base.playerCollider = playerCollider;
            base.playerHealth = playerHealth;
            base.playerController = playerController;
        }

        public override void CheckCollisions()
        {
            if (Math.Abs(transform.position.x - playerCollider.center.x) < (spriteRenderer.scaledWidth / 2 + Math.Abs(playerCollider.center.x - playerCollider.max.x)) &&
                Math.Abs(transform.position.y - playerCollider.center.y) < (spriteRenderer.scaledHeight / 2 + Math.Abs(playerCollider.center.y - playerCollider.max.y)) &&
                playerController.IsMoving())
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
    
    public class OrangeBoneAttack : BaseBoneAttack
    {
        public OrangeBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            base.speed = speed;
            transform = new Transform(position);
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage("assets/Sprites/Orange bone attack.png"));
            collider = new BoxCollider(transform, spriteRenderer);
            base.playerCollider = playerCollider;
            base.playerHealth = playerHealth;
            base.playerController = playerController;
        }

        public override void CheckCollisions()
        {
            if (Math.Abs(transform.position.x - playerCollider.center.x) < (spriteRenderer.scaledWidth / 2 + Math.Abs(playerCollider.center.x - playerCollider.max.x)) &&
                Math.Abs(transform.position.y - playerCollider.center.y) < (spriteRenderer.scaledHeight / 2 + Math.Abs(playerCollider.center.y - playerCollider.max.y)) &&
                !playerController.IsMoving())
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
