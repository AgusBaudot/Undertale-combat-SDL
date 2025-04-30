using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
        private int numOfAttacks = 0;
        private int selectAttack = 2;

        private bool up = true;

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
            if (selectAttack == 1)
            {
                if (counter > 1)
                {
                    attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 100), Vector2.right * 5, player.GetCollider(), player.healthController, enemy));
                    attackListLeft.Add(new EnemyAttack(new Vector2(880, Engine.center.y + -100), Vector2.left * 5, player.GetCollider(), player.healthController, enemy));
                    counter = 0;
                }
            }
            else if (selectAttack == 2)
            {
                if (counter > 0.4)
                {
                    if (up)
                    {
                        attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y - 100), Vector2.right * 10, player.GetCollider(), player.healthController, enemy));
                        up = !up;
                        counter = 0;
                    }
                    else
                    {
                        attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 100), Vector2.right * 10, player.GetCollider(), player.healthController, enemy));
                        up = !up;
                        counter = 0;
                    }
                    
                    
                }
            }
            
        }

        private void AttackBehavior()
        {
            if (selectAttack == 1)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Update();
                    attackListLeft[i].Update();
                }
            }

            else if (selectAttack == 2)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Update();
                }
            }

        }

        public void Render()
        {
            if (selectAttack == 1)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Render();
                    attackListLeft[i].Render();
                }
            }

            else if (selectAttack == 2)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Render();
                }
            }

        }

        private void RemoveAttack()
        {
            if (selectAttack == 1)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    if (attackListRight[i].transform.position.x > 880)
                    {
                        attackListRight.Remove(attackListRight[i]);
                    }
                }
                for (int i = 0; i < attackListLeft.Count; i++)
                {
                    if (attackListLeft[i].transform.position.x < 160)
                    {
                        attackListLeft.Remove(attackListLeft[i]);
                        numOfAttacks++;
                    }

                    if (numOfAttacks > 6)
                    {
                        ResetListAttack();
                        numOfAttacks = 0;
                        break;
                    }
                }
            }

            else if (selectAttack == 2)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    if (attackListRight[i].transform.position.x > 880)
                    {
                        attackListRight.Remove(attackListRight[i]);
                        numOfAttacks++;
                    }

                    if (numOfAttacks > 12)
                    {
                        ResetListAttack();
                        numOfAttacks = 0;
                        break;
                    }
                }
            }

        }

        public void ResetListAttack()
        {
            attackListRight.Clear();
            attackListLeft.Clear();

            instance.OnGameStateChanged(GameState.PlayerTurn);
            //instance.OnGameStateChanged((instance.GetGameState() == (GameState)2) ? (GameState)3 : (GameState)2); //Toggle between gamestate 2 & 3
        }

    }


}
