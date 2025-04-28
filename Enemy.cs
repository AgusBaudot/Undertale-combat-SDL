using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy
    {
        private Vector2 position;
        private int health = 50;
        private int maxHealth = 50;
        private Player player;
        private Transform transform;
        private SpriteRenderer renderer;
        public HealthController healthController;
        private List<EnemyAttack> attackListRight = new List<EnemyAttack>();
        private List<EnemyAttack> attackListLeft = new List<EnemyAttack>();

        private float counter = 0;

        private Animator anim;

        public Enemy(Player player, float x, float y)
        {
            this.player = player;
            position = new Vector2(x, y);
            transform = new Transform(position);
            healthController = new HealthController(health, maxHealth, 0.5f);
            List<Image> spriteSheet = new List<Image>();
            for (int i = 1; i <= 5; i++)
            {
                spriteSheet.Add(Engine.LoadImage($"assets/Spritesheet_{i}.png"));
            }
            renderer = new SpriteRenderer (transform, spriteSheet[0]);
            anim = new Animator(spriteSheet, 0.15f, renderer, true);
        }

        public void Update(GameState currentState)
        {
            healthController.Update();
            if (currentState == GameState.EnemyTurn)
            {
                counter += Time.deltaTime;

                if (counter > 1)
                {
                    attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 100), Vector2.right * 5, player.GetCollider(), player.healthController, this));
                    attackListLeft.Add(new EnemyAttack(new Vector2(900, Engine.center.y + -100), Vector2.left * 5, player.GetCollider(), player.healthController, this));
                    counter = 0;
                } 
            }
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < attackListRight.Count; i++)
            {
                attackListRight[i].Update();
                attackListLeft[i].Update();
            }

            //if (attackListRight.Count > 0)
            //{
            //    foreach (EnemyAttack attack in attackListRight)
            //    {
            //        attack.Update();
            //    } 
            //}
            //if (attackListLeft.Count > 0)
            //{
            //    foreach (EnemyAttack attack in attackListRight)
            //    {
            //        attack.Update();
            //    }
            //}
        }

        public void Render(GameState state)
        {
            anim.Update();
            anim.OnAnimationEnd += AnimationEnded;
            if (state == GameState.EnemyTurn)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Render();
                    attackListLeft[i].Render();
                }

                //if (attackListRight.Count > 0)
                //{
                //    foreach (EnemyAttack attack in attackListRight)
                //    {
                //        attack.Render();
                //    } 
                //}

                //if (attackListLeft.Count > 0)
                //{
                //    foreach(EnemyAttack attack in attackListRight)
                //    {
                //        attack.Render();
                //    } 
                //}
            }
        }

        private void AnimationEnded()
        {
            //Animation has ended.
        }

        public void RemoveAttack(EnemyAttack attack)
        {
            //if (attackListLeft.Contains(attack))
            //{
            //    attackListLeft.Remove(attack);
            //    Engine.Debug("left");
            //}
            //else
            //{
            //    Engine.Debug("right");
            //    attackListRight.Remove(attack);
            //}
            
            
            //Engine.Debug($"Removed {attack}");
        }
    }
}
