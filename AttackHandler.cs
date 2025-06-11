using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
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

        private IAbstractFactory whiteFactory, blueFactory, orangeFactory;
        private IAttackPatterns currentAttackPattern;
        private Dictionary<int, IAttackPatterns> attackPatterns;

        private Sdl.SDL_Rect clipRect;
        private Random rng = new Random();
        #endregion
        #endregion

        #region Logic
        public AttackHandler(Player player, Enemy enemy, CombatArea combatArea) //AttackHandler constructor.
        {
            this.player = player;
            this.enemy = enemy;
            this.combatArea = combatArea;
            whiteFactory = new WhiteBoneFactory();
            blueFactory = new BlueBoneFactory();
            orangeFactory = new OrangeBoneFactory();
            instance = GameManager.GetInstance();
            SetAreaRect();

            attackPatterns = new Dictionary<int, IAttackPatterns>()
            {
                {1, new FirstBoneAttack(player, enemy, whiteFactory) },
                {2, new SecondBoneAttack(player, enemy, whiteFactory) },
                {3, new ThirdBoneAttack(player, enemy, whiteFactory) },
                {4, new FourthBoneAttack(player, enemy, whiteFactory) },
                {5, new FifthBoneAttack(player, enemy, whiteFactory) },
                {6, new FirstBlueBoneAttack(player, enemy, blueFactory, orangeFactory) },
            };
            currentAttackPattern = attackPatterns[selectAttack];
            attackPatterns[selectAttack].OnAttackEnd += AdvanceAttackPhase;
        }
        public void Update()
        {
            counter += Time.deltaTime; //Update timer.
            if (selectAttack == 1 || selectAttack == 6)
            {
                duration += Time.deltaTime; //Update duration timer.
            }

            SpawnAttack(); //Spawn attacks.
            RemoveAttack(); //Remove any unnecesary attacks from lists.
        }

        public void FixedUpdate() => AttackBehavior();
        public void Render() => currentAttackPattern.RenderList(GetActiveAttackList(), ref clipRect);
        private void SpawnAttack() //Spawner of attacks
        {
            if (selectAttack == 1)
            {
                currentAttackPattern.SpawnAttack(attackListRight, attackListLeft, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
            }
            else if (selectAttack == 6)
            {
                currentAttackPattern.SpawnAttack(attackListRight, attackListLeft, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
            }
            else
            {
                currentAttackPattern.SpawnAttack(GetActiveAttackList(), null, ref counter, ref duration, ref numOfAttacks, ref up, ref selectPosition);
            }
        }
        private void AttackBehavior() //Move logic of each attack
        {
            currentAttackPattern.UpdateAttack(GetActiveAttackList(), ref duration);
        } 
        #endregion
        #region Helpers
        private void RemoveAttack()
        {
            if (selectAttack == 1)
            {
                currentAttackPattern.RemoveAttack(attackListRight, attackListLeft);
            }
            else if (selectAttack == 6)
            {
                currentAttackPattern.RemoveAttack(attackListRight, attackListLeft);
            }
            else
            {
                currentAttackPattern.RemoveAttack(GetActiveAttackList(), null);
            }
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
                6 => attackListRight.Concat(attackListLeft).ToList(),
                _ => new List<BaseBoneAttack>()
            };
        }
        private void AdvanceAttackPhase()
        {
            ResetLists();
            attackPatterns[selectAttack].OnAttackEnd -= AdvanceAttackPhase;
            numOfAttacks = 0;
            duration = 0;
            lastAttack = selectAttack;
            while (selectAttack == lastAttack)
            {
                selectAttack = rng.Next(1, 7);
            }
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
