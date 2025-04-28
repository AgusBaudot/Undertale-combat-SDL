using System;
using System.Collections.Generic;
using System.Linq;
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

            if (counter > 1)
            {
                attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 100), Vector2.right * 5, player.GetCollider(), player.healthController, enemy));
                attackListLeft.Add(new EnemyAttack(new Vector2(900, Engine.center.y + -100), Vector2.left * 5, player.GetCollider(), player.healthController, enemy));
                counter = 0;
            }
        }

        public void FixedUpdate()
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
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight.Remove(attackListRight[i]);
                    attackListLeft.Remove(attackListLeft[i]);
                }

                Engine.Debug(attackListRight.Count.ToString());
                Engine.Debug(attackListLeft.Count.ToString());

                instance.OnGameStateChanged((instance.GetGameState() == (GameState)2) ? (GameState)3 : (GameState)2); //Toggle between gamestate 2 & 3
            }
        }
    }


}
