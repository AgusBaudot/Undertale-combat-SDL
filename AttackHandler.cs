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
        #region Classes
        private Player player;
        private Enemy enemy;
        private GameManager instance;
        #endregion
        #region Internal variables
        #region Attack lists
        private List<EnemyAttack> attackListRight = new List<EnemyAttack>();
        private List<EnemyAttack> attackListLeft = new List<EnemyAttack>();
        private List<EnemyAttack> attackListDown = new List<EnemyAttack>();
        #endregion
        #region Attack logic
        private float counter = 0; //Time passed since last spawned attack
        private float duration = 0; //Time passsed since start of attack

        private int numOfAttacks = 0;
        private int selectAttack = 1;

        private int selectPosition = 0;
        private bool up = true;
        #endregion
        #endregion

        public AttackHandler (Player player, Enemy enemy) //AttackHandler constructor.
        {
            this.player = player; 
            this.enemy = enemy;
            instance = GameManager.GetInstance();
        }

        public void Update ()
        {
            counter += Time.deltaTime; //Update timer.
            if (selectAttack == 1)  
            {
                duration += Time.deltaTime; //Update duration timer.
            }

            SpawnAttack(); //Spawn attacks.
            RemoveAttack(); //Remove any unnecesary attacks from lists.
        }

        public void FixedUpdate()
        {
            AttackBehavior(); //Move each attack.
        }

        private void SpawnAttack() //Spawner of attacks
        {
            switch (selectAttack)
            {
                case 1: //If enemy is doing his first attack:
                    if (counter > 1 - (duration/20)) //If 1 - (duration/20)" have passed since last attack was thrown:
                    {
                        attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 90), Vector2.right * 5, player.GetCollider(), player.healthController, enemy));
                        attackListLeft.Add(new EnemyAttack(new Vector2(880, Engine.center.y + -90), Vector2.left * 5, player.GetCollider(), player.healthController, enemy));
                        //Add new attack to left and right lists.
                        counter = 0; //Reset attack timer.
                    }
                    break;
                case 2:
                    if (counter > 0.4) //If 0.4" have passed since last attack was thrown:
                    {
                        if (up) //If previous attack was in bottom half of sceen:
                        {
                            attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y - 90), Vector2.right * 10, player.GetCollider(), player.healthController, enemy));
                            up = !up;
                            //Add new attack to right list and set up to inverse value.
                        }
                        else //If previous attack was in top hald of screen:
                        {
                            attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 90), Vector2.right * 10, player.GetCollider(), player.healthController, enemy));
                            up = !up;
                            //Add new attack to right list and set up to inverse value.
                        }
                        counter = 0; //Reset attack timer.
                    }
                    break;
                case 3:
                    if (counter > 0.3) //If 0.3" have passed since last attack was thrown:
                    {
                        switch (selectPosition) 
                        {
                            //Spawn attack along x axis depending on position of previous attack and add it to list.
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
                        counter = 0; //Reset attack timer.
                    }
                    break;
            }
        }

        private void AttackBehavior() //Move logic of each attack
        {
            switch (selectAttack)
            {
                case 1:
                    for (int i = 0; i < attackListRight.Count; i++) //If enemy is performing his first attack, update each attack of both right and left lists.
                    {
                        attackListRight[i].UpdateSpeed(Vector2.right * (5 + duration)); 
                        attackListLeft[i].UpdateSpeed(Vector2.left * (5 + duration));
                        attackListRight[i].Update();
                        attackListLeft[i].Update();
                    }
                    break;
                case 2:
                    for (int i = 0; i < attackListRight.Count; i++) //If enemy is perfoming his second attack, update each attack of right list.
                    {
                        attackListRight[i].Update();
                    }
                    break;
                case 3:
                    for (int i = 0; i < attackListDown.Count; i++) //If enemy is performing his third attack, update each attack of down list.
                    {
                        attackListDown[i].Update();
                    }
                    break;
            }
        }
        public void Render() //Render each attack on screen. See AttackBehaviour method for logic.
        {
            switch (selectAttack) //Call Render method of each attack instead of Update.
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
        private void RemoveAttack() //Remove unnecesary attacks from lists.
        {
            switch (selectAttack)
            {
                case 1:
                    for (int i = 0; i < attackListRight.Count; i++)
                    {
                        if (attackListRight[i].transform.position.x > 880) //Remove attacks from right list exiting in right side of screen.
                        {
                            attackListRight.Remove(attackListRight[i]);
                        }
                    }
                    for (int i = 0; i < attackListLeft.Count; i++) //Remove attacks from left list existing in left side of scren.
                    {
                        if (attackListLeft[i].transform.position.x < 160)
                        {
                            attackListLeft.Remove(attackListLeft[i]);
                            numOfAttacks++;
                        }

                        if (numOfAttacks > 8)
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
        private void ResetListAttack()
        {
            attackListRight.Clear();
            attackListLeft.Clear();
            attackListDown.Clear();

            if (selectAttack == 1) selectAttack = 2;
            else if (selectAttack == 2) selectAttack = 3;
            else if (selectAttack == 3)
            {
                selectAttack = 1;
                duration = 0; //Reset duration timer.
            }

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
