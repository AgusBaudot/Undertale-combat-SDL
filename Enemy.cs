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
        private AttackHandler attackHandler;

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
            attackHandler = new AttackHandler(player, this);
        }

        public void Update(GameState currentState)
        {
            attackHandler.ResetListAttack();

            healthController.Update();
            if (currentState == GameState.EnemyTurn)
            {
                
                attackHandler.Update();
            }
        }

        public void FixedUpdate()
        {
            attackHandler?.FixedUpdate();

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
                attackHandler.Render();

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
