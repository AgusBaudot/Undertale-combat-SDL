using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
        private List<BaseBoneAttack> attackListRight = new List<BaseBoneAttack >();
        private List<BaseBoneAttack> attackListLeft = new List<BaseBoneAttack>();
        private List<BaseBoneAttack> attackListDown = new List<BaseBoneAttack>();
        private List<BaseBoneAttack> attackListUp = new List<BaseBoneAttack>();
        #endregion
        #region Attack logic
        private float counter = 0; //Time passed since last spawned attack
        private float duration = 0; //Time passsed since start of attack

        private int numOfAttacks = 0;
        private int selectAttack = 1;
        private int lastAttack = 0;

        private int selectPosition = 0;
        private bool up = true;
        #endregion
        #endregion

        private ConcreteFactory factory;
        private IAttackPatterns currentAttackPattern;
        private Dictionary<int, IAttackPatterns> attackPatterns;

        private Sdl.SDL_Rect clipRect;
        private Random rng = new Random();

        #region Logic
        public AttackHandler(Player player, Enemy enemy, CombatArea combatArea) //AttackHandler constructor.
        {
            this.player = player;
            this.enemy = enemy;
            this.combatArea = combatArea;
            factory = new ConcreteFactory();
            instance = GameManager.GetInstance();
            SetAreaRect();

            attackPatterns = new Dictionary<int, IAttackPatterns>()
            {
                {1, new FirstBoneAttack(player, enemy, factory) },
                {2, new SecondBoneAttack(player, enemy, factory) },
                {3, new ThirdBoneAttack(player, enemy, factory) },
                {4, new FourthBoneAttack(player, enemy, factory) },
                {5, new FifthBoneAttack(player, enemy, factory) },
            };
            currentAttackPattern = attackPatterns[selectAttack];
            attackPatterns[selectAttack].OnAttackEnd += AdvanceAttackPhase;
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
            
            //Engine.Debug($"Right list count: {attackListRight.Count} | Left list count: {attackListLeft.Count}");

            //if (selectAttack == 1)
            //{
            //    currentAttackPattern.SpawnAttack(attackListUp.Concat(attackListLeft).ToList(), ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
            //    currentAttackPattern.UpdateAttack((List<WhiteBoneAttack>)attackListUp.Concat(attackListLeft), ref duration);
            //    currentAttackPattern.RemoveAttack((List<WhiteBoneAttack>)attackListUp.Concat(attackListLeft), ref numOfAttacks);
            //}
        }
        public void FixedUpdate() => AttackBehavior();
        public void Render() //Render each attack on screen. See AttackBehaviour method for logic.
        {
            //Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            //foreach (var attack in GetActiveAttackList())
            //{
            //    attack.Render();
            //}
            //Sdl.SDL_Rect screenRect =
            //    new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            //Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
            currentAttackPattern.RenderList(GetActiveAttackList(), ref clipRect);
        }
        private void SpawnAttack() //Spawner of attacks
        {
            switch (selectAttack)
            {
                case 1: //If enemy is doing his first attack:
                    /*
                     if (counter > 1.2f - (duration / 20)) //If 1 - (duration/20)" have passed since last attack was thrown:
                    {
                        AddAttack(attackListRight, new Vector2(160, Engine.center.y + 90), Vector2.right * 5);
                        AddAttack(attackListLeft, new Vector2(880, Engine.center.y - 90), Vector2.left * 5);
                        counter = 0; //Reset attack timer.
                    } //Delete if successful.
                     */
                    currentAttackPattern.SpawnAttack(attackListRight, attackListLeft, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
                    break;
                case 2:
                    //if (counter > 0.4) //If 0.4" have passed since last attack was thrown:
                    //{
                    //    float yOffset = up ? -90 : 90;
                    //    AddAttack(attackListRight, new Vector2(160, Engine.center.y + yOffset), Vector2.right * 10);
                    //    up = !up;
                    //    counter = 0;
                    //}
                    currentAttackPattern.SpawnAttack(attackListRight, null, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
                    break;
                case 3:
                    //if (counter > 0.3) //If 0.3" have passed since last attack was thrown:
                    //{
                    //    float xPos = 200 + 125 * selectPosition;
                    //    AddAttack(attackListDown, new Vector2(xPos, Engine.center.y - 100), Vector2.down * 10);
                    //    selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                    //    counter = 0;
                    //}
                    currentAttackPattern.SpawnAttack(attackListDown, null, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
                    break;
                case 4:
                    //if (counter > 0.4)
                    //{
                    //    float yOffset = up ? -90 : 90;
                    //    AddAttack(attackListLeft, new Vector2(880, Engine.center.y + yOffset), Vector2.left * 10);
                    //    up = !up;
                    //    counter = 0;
                    //}
                    currentAttackPattern.SpawnAttack(attackListLeft, null, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
                    break;
                case 5:
                    //if (counter > 0.3)
                    //{
                    //    float xPos = 840 - 125 * selectPosition;
                    //    AddAttack(attackListUp, new Vector2(xPos, Engine.center.y + 100), Vector2.up * 10);
                    //    selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                    //    counter = 0;
                    //}
                    currentAttackPattern.SpawnAttack(attackListUp, null, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
                    break;
            }
        }
        private void AttackBehavior() //Move logic of each attack
        {
            switch (selectAttack)
            {
                case 1:
                    currentAttackPattern.UpdateAttack(GetActiveAttackList(), ref duration);
                    break;
                case 2:
                    //attackListRight.ForEach(a => a.Update());
                    currentAttackPattern.UpdateAttack(attackListRight, ref duration);
                    break;
                case 3:
                    //attackListDown.ForEach(a => a.Update());
                    currentAttackPattern.UpdateAttack(attackListDown, ref duration);
                    break;
                case 4:
                    //attackListLeft.ForEach(a => a.Update());
                    currentAttackPattern.UpdateAttack(attackListLeft, ref duration);
                    break;
                case 5:
                    //attackListUp.ForEach(a => a.Update());
                    currentAttackPattern.UpdateAttack(attackListUp, ref duration);
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
                    currentAttackPattern.RemoveAttack(attackListRight, attackListLeft);
                    break;
                case 2:
                    //RemoveAttacks(attackListRight, a => a.transform.position.x > 880);
                    currentAttackPattern.RemoveAttack(GetActiveAttackList(), null);
                    break;
                case 3:
                    currentAttackPattern.RemoveAttack(GetActiveAttackList(), null);
                    //RemoveAttacks(attackListDown, a => a.transform.position.y > 500);
                    break;
                case 4:
                    currentAttackPattern.RemoveAttack(GetActiveAttackList(), null);
                    //RemoveAttacks(attackListLeft, a => a.transform.position.x < 160);
                    break;
                case 5:
                    currentAttackPattern.RemoveAttack(GetActiveAttackList(), null);
                    //RemoveAttacks(attackListUp, a => a.transform.position.y < Engine.center.y - 100);
                    break;
            }
        }
        private void RemoveAttacks(List<BaseBoneAttack> list, System.Predicate<BaseBoneAttack> condition) //Remove unnecesary attacks from lists.
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
        private void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new WhiteBoneAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
        private List<BaseBoneAttack> GetActiveAttackList()
        {
            return selectAttack switch //Upgraded to c# 8.0
            {
                1 => attackListRight.Concat(attackListLeft).ToList(), //Adjust interface to get both lists separately, and not use concatenate every frame
                2 => attackListRight,
                3 => attackListDown,
                4 => attackListLeft,
                5 => attackListUp,
                _ => new List<BaseBoneAttack>()
            };
        }
        private void AdvanceAttackPhase()
        {
            ResetLists();
            attackPatterns[selectAttack].OnAttackEnd -= AdvanceAttackPhase;
            numOfAttacks = 0;
            //selectAttack = selectAttack == 3 ? 1 : selectAttack + 1; //Weird behaviour due to increment after comparison.
            //selectAttack = (int)Helpers.Wrap(selectAttack + 1, 1, 6);
            lastAttack = selectAttack;
            while (selectAttack == lastAttack)
            {
                selectAttack = rng.Next(1, 6);
            }
            if (selectAttack == 1) duration = 0;
            instance.OnGameStateChanged(GameState.PlayerTurn);

            attackPatterns[selectAttack].OnAttackEnd += AdvanceAttackPhase;
            if (attackPatterns.ContainsKey(selectAttack) && currentAttackPattern != attackPatterns[selectAttack])
            {
                currentAttackPattern = attackPatterns[selectAttack];
            }
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
