using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class AttackHandler
    {
        private Player player;
        private Enemy enemy;
        private float counter = 0;
        private List<EnemyAttack> attackListRight = new List<EnemyAttack>();
        private List<EnemyAttack> attackListLeft = new List<EnemyAttack>();
        private GameManager instance;

        public AttackHandler (Player player, Enemy enemy)
        {
            this.player = player;   
            this.enemy = enemy;
            instance = GameManager.GetInstance();
        }

        public void Update ()
        {
            counter += Time.deltaTime;

            SpawnAttack();
            RemoveAttack();

        }

        public void FixedUpdate()
        {
            AttackBehavior();
        }

        private void SpawnAttack()
        {
            if (counter > 1)
            {
                attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 100), Vector2.right * 5, player.GetCollider(), player.healthController, enemy));
                attackListLeft.Add(new EnemyAttack(new Vector2(880, Engine.center.y + -100), Vector2.left * 5, player.GetCollider(), player.healthController, enemy));
                counter = 0;
            }
        }

        private void AttackBehavior()
        {
            for (int i = 0; i < attackListRight.Count; i++)
            {
                attackListRight[i].Update();
                attackListLeft[i].Update();
            }
        }

        public void Render()
        {
            for (int i = 0; i < attackListRight.Count; i++)
            {
                attackListRight[i].Render();
                attackListLeft[i].Render();
            }
        }

        public void ResetListAttack()
        {
            if (Engine.GetKeyDown(Engine.KEY_P))
            {
                attackListRight.Clear();
                attackListLeft .Clear();

                instance.OnGameStateChanged((instance.GetGameState() == (GameState)2) ? (GameState)3 : (GameState)2); //Toggle between gamestate 2 & 3
            }
        }

        private void RemoveAttack()
        {
            for (int i = 0; i < attackListRight.Count; i++)
            {
                if (attackListRight[i].transform.position.x > 880)
                {
                    attackListRight.Remove(attackListRight[i]);
                }
            }
            List<EnemyAttack> toRemove = new List<EnemyAttack>();
            for (int i = 0; i < attackListLeft.Count; i++)
            {
                if (attackListLeft[i].transform.position.x < 160)
                {
                    attackListLeft.Remove(attackListLeft[i]);
                }
            }
        }

    }


}
