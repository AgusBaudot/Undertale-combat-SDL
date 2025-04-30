using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
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
        private List<EnemyAttack> attackListDown = new List<EnemyAttack>();
        private GameManager instance;

        private int numOfAttacks = 0;
        private int selectAttack = 1;

        private int selectPosition = 0;
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
            switch (selectAttack)
            {
                case 1:
                    if (counter > 1)
                    {
                        attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 90), Vector2.right * 5, player.GetCollider(), player.healthController, enemy));
                        attackListLeft.Add(new EnemyAttack(new Vector2(880, Engine.center.y + -90), Vector2.left * 5, player.GetCollider(), player.healthController, enemy));
                        counter = 0;
                    }
                    break;
                case 2:
                    if (counter > 0.4)
                    {
                        if (up)
                        {
                            attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y - 90), Vector2.right * 10, player.GetCollider(), player.healthController, enemy));
                            up = !up;
                        }
                        else
                        {
                            attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 90), Vector2.right * 10, player.GetCollider(), player.healthController, enemy));
                            up = !up;
                        }

                        counter = 0;
                    }
                    break;
                case 3:
                    if (counter > 0.3)
                    {
                        switch (selectPosition)
                        {
                            case 0:
                                attackListDown.Add(new EnemyAttack(new Vector2(200, Engine.center.y + -100), Vector2.down * 10, player.GetCollider(), player.healthController, enemy));
                                selectPosition = 1;
                                break;
                            case 1:

                                attackListDown.Add(new EnemyAttack(new Vector2(325, Engine.center.y + -100), Vector2.down * 10, player.GetCollider(), player.healthController, enemy));
                                selectPosition = 2;
                                break;
                            case 2:

                                attackListDown.Add(new EnemyAttack(new Vector2(450, Engine.center.y + -100), Vector2.down * 10, player.GetCollider(), player.healthController, enemy));
                                selectPosition = 3;
                                break;
                            case 3:

                                attackListDown.Add(new EnemyAttack(new Vector2(575, Engine.center.y + -100), Vector2.down * 10, player.GetCollider(), player.healthController, enemy));
                                selectPosition = 4;
                                break;
                            case 4:

                                attackListDown.Add(new EnemyAttack(new Vector2(700, Engine.center.y + -100), Vector2.down * 10, player.GetCollider(), player.healthController, enemy));
                                selectPosition = 5;
                                break;
                            case 5:

                                attackListDown.Add(new EnemyAttack(new Vector2(825, Engine.center.y + -100), Vector2.down * 10, player.GetCollider(), player.healthController, enemy));
                                selectPosition = 0;
                                break;
                        }
                        counter = 0;
                    }
                    break;
            }
        }

        private void AttackBehavior()
        {
            switch (selectAttack)
            {
                case 1:
                    for (int i = 0; i < attackListRight.Count; i++)
                    {
                        attackListRight[i].Update();
                        attackListLeft[i].Update();
                    }
                    break;
                case 2:
                    for (int i = 0; i < attackListRight.Count; i++)
                    {
                        attackListRight[i].Update();
                    }
                    break;
                case 3:
                    for (int i = 0; i < attackListDown.Count; i++)
                    {
                        attackListDown[i].Update();
                    }
                    break;
            }
        }
        public void Render()
        {
            switch (selectAttack)
            {
                case 1:
                    for (int i = 0; i < attackListRight.Count; i++)
                    {
                        attackListRight[i].Render();
                        attackListLeft[i].Render();
                    }
                    break;
                case 2:
                    for (int i = 0; i < attackListRight.Count; i++)
                    {
                        attackListRight[i].Render();
                    }
                    break;
                case 3:
                    for (int i = 0; i < attackListDown.Count; i++)
                    {
                        attackListDown[i].Render();
                    }
                    break;
            }
        }
        private void RemoveAttack()
        {
            switch (selectAttack)
            {
                case 1:
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
                    break;
                case 2:
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
                    break;
                case 3:
                    for (int i = 0; i < attackListDown.Count; i++)
                    {
                        if (attackListDown[i].transform.position.y > 500)
                        {
                            attackListDown.Remove(attackListDown[i]);
                            numOfAttacks++;
                        }

                        if (numOfAttacks > 15)
                        {
                            ResetListAttack();
                            numOfAttacks = 0;
                            selectPosition = 0;
                            break;
                        }
                    }
                    break;
            }
        }
        public void ResetListAttack()
        {
            attackListRight.Clear();
            attackListLeft.Clear();
            attackListDown.Clear();

            if (selectAttack == 1) selectAttack = 2;
            else if (selectAttack == 2) selectAttack = 3;
            else if (selectAttack == 3) selectAttack = 1;

            instance.OnGameStateChanged(GameState.PlayerTurn);
        }
        public void Reset()
        {
            counter = 0;
            numOfAttacks = 0;
            selectAttack = 1;
            selectPosition = 0;
            up = true;
            attackListRight.Clear();
            attackListLeft.Clear();
            attackListDown.Clear();
        }
    }
}
