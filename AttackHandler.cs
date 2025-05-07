using System.Collections.Generic;
using System.Linq;
using Tao.Sdl;

namespace MyGame
{
    public class AttackHandler
    {
        #region Classes
        private Player player;
        private Enemy enemy;
        private CombatArea combatArea;
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
        Sdl.SDL_Rect clipRect;

        #region Logic
        public AttackHandler(Player player, Enemy enemy, CombatArea combatArea) //AttackHandler constructor.
        {
            this.player = player;
            this.enemy = enemy;
            this.combatArea = combatArea;
            instance = GameManager.GetInstance();
            SetAreaRect();
        }
        public void Update()
        {
            counter += Time.deltaTime; //Update timer.
            if (selectAttack == 1)
            {
                duration += Time.deltaTime; //Update duration timer.
            }

            SpawnAttack(); //Spawn attacks.
            RemoveAttack(); //Remove any unnecesary attacks from lists.
        }
        public void FixedUpdate() => AttackBehavior();
        public void Render() //Render each attack on screen. See AttackBehaviour method for logic.
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in GetActiveAttackList())
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect =
                new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }
        private void SpawnAttack() //Spawner of attacks
        {
            switch (selectAttack)
            {
                case 1: //If enemy is doing his first attack:
                    if (counter > 1.2f - (duration / 20)) //If 1 - (duration/20)" have passed since last attack was thrown:
                    {
                        AddAttack(attackListRight, new Vector2(160, Engine.center.y + 90), Vector2.right * 5);
                        AddAttack(attackListLeft, new Vector2(880, Engine.center.y - 90), Vector2.left * 5);
                        counter = 0; //Reset attack timer.
                    }
                    break;
                case 2:
                    if (counter > 0.4) //If 0.4" have passed since last attack was thrown:
                    {
                        float yOffset = up ? -90 : 90;
                        AddAttack(attackListRight, new Vector2(160, Engine.center.y + yOffset), Vector2.right * 10);
                        up = !up;
                        counter = 0;
                    }
                    break;
                case 3:
                    if (counter > 0.3) //If 0.3" have passed since last attack was thrown:
                    {
                        float xPos = 200 + 125 * selectPosition;
                        AddAttack(attackListDown, new Vector2(xPos, Engine.center.y - 100), Vector2.down * 10);
                        selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                        counter = 0;
                    }
                    break;
            }
        }
        private void AttackBehavior() //Move logic of each attack
        {
            switch (selectAttack)
            {
                case 1:
                    foreach (var attack in attackListRight)
                    {
                        attack.UpdateSpeed(Vector2.right * (3 + duration));
                        attack.Update();
                    }
                    foreach (var attack in attackListLeft)
                    {
                        attack.UpdateSpeed(Vector2.left * (3 + duration));
                        attack.Update();
                    }
                    break;
                case 2:
                    attackListRight.ForEach(a => a.Update());
                    break;
                case 3:
                    attackListDown.ForEach(a => a.Update());
                    break;
            }
        } 
        #endregion
        #region Helpers
        private void RemoveAttack()
        {
            switch (selectAttack)
            {
                case 1:
                    RemoveAttacks(attackListRight, a => a.transform.position.x > 880);
                    RemoveAttacks(attackListLeft, a => a.transform.position.x < 165);
                    if (numOfAttacks > 16) AdvanceAttackPhase();
                    break;
                case 2:
                    RemoveAttacks(attackListRight, a => a.transform.position.x > 880);
                    if (numOfAttacks > 12) AdvanceAttackPhase();
                    break;
                case 3:
                    RemoveAttacks(attackListDown, a => a.transform.position.y > 500);
                    if (numOfAttacks > 18)
                    {
                        selectPosition = 0;
                        AdvanceAttackPhase();
                    }
                    break;
            }
        }
        private void RemoveAttacks(List<EnemyAttack> list, System.Predicate<EnemyAttack> condition) //Remove unnecesary attacks from lists.
        {
            foreach (var attack in list.ToList())
            {
                if (condition(attack))
                {
                    list.Remove(attack);
                    numOfAttacks++;
                }
            }
        }
        private void AddAttack(List<EnemyAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new EnemyAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
        private List<EnemyAttack> GetActiveAttackList()
        {
            return selectAttack switch //Upgraded to c# 8.0
            {
                1 => attackListRight.Concat(attackListLeft).ToList(),
                2 => attackListRight,
                3 => attackListDown,
                _ => new List<EnemyAttack>()
            };
        }
        private void AdvanceAttackPhase()
        {
            ResetLists();
            numOfAttacks = 0;
            //selectAttack = selectAttack == 3 ? 1 : selectAttack + 1; //Weird behaviour due to increment after comparison.
            selectAttack = (int)Helpers.Wrap(selectAttack + 1, 1, 4);
            if (selectAttack == 1) duration = 0;
            instance.OnGameStateChanged(GameState.PlayerTurn);
        }
        public void Reset()
        {
            counter = 0;
            numOfAttacks = 0;
            selectAttack = 1;
            selectPosition = 0;
            duration = 0;
            up = true;
            ResetLists();
        }
        private void ResetLists()
        {
            attackListRight.Clear();
            attackListLeft.Clear();
            attackListDown.Clear();
        } 
        private void SetAreaRect()
        {
            var areaCenter = combatArea.bgTransform.position;
            var areaHalfSize = combatArea.GetAreaLimits() / 2;
            clipRect = new Sdl.SDL_Rect()
            {
                x = (short)(areaCenter.x - areaHalfSize.x),
                y = (short)(areaCenter.y - areaHalfSize.y),
                w = (short)(areaHalfSize.x * 2),
                h = (short)(areaHalfSize.y * 2)
            };
        }
        #endregion
    }
}
