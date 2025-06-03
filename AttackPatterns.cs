using System.Linq;
using System.Collections.Generic;
using Tao.Sdl;
using System;
using System.Diagnostics;

namespace MyGame
{
    public abstract class BaseAttackPattern : IAttackPatterns
    {
        protected Player player;
        protected Enemy enemy;
        protected IAbstractFactory factory;

        public event Action OnAttackEnd;

        protected BaseAttackPattern(Player player, Enemy enemy, IAbstractFactory factory)
        {
            this.player = player;
            this.enemy = enemy;
            this.factory = factory;
        }

        protected void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(factory.CreateBoneAttack(position, direction, player.GetCollider(), player.healthController, player.GetPlayerController(), enemy));
        }

        protected void RenderAttacks(List<BaseBoneAttack> attackList, ref Tao.Sdl.Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
                attack.Render();
            Sdl.SDL_Rect screenRect = new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        public abstract void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB,
            ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition);

        public virtual void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            attackList.ForEach(a => a.Update());
        }

        public abstract void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB);

        public virtual void RenderList(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            RenderAttacks(attackList, ref clipRect);
        }

        protected void EndIfFinished(bool condition)
        {
            if (condition)
                OnAttackEnd?.Invoke();
        }
    }

    public class FirstBoneAttack : BaseAttackPattern //Attack for spawning left and right attacks at the same time.
    {
        public FirstBoneAttack(Player player, Enemy enemy, IAbstractFactory factory) : base(player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 16 && listA.Count == 0 && listB.Count == 0)
            {
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 16) return;
            if (counter > 1.2f - (duration / 20)) //If 1 - (duration/20)" have passed since last attack was thrown:
            {
                AddAttack(listA, new Vector2(160, Engine.center.y + 90), Vector2.right * 5);
                AddAttack(listB, new Vector2(880, Engine.center.y - 90), Vector2.left * 5);
                counter = 0; //Reset attack timer.
                numOfAttacks += 2;
            }
        }

        public override void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            foreach (BaseBoneAttack attack in attackList)
            {
                Vector2 currentDir = attack.speed.normalized;
                attack.UpdateSpeed(currentDir * (3 + duration));
                attack.Update();
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x > 880);
            listB.RemoveAll(a => a.transform.position.x < 160);
        }
    }

    public class SecondBoneAttack : BaseAttackPattern
    {
        public SecondBoneAttack(Player player, Enemy enemy, IAbstractFactory factory) : base (player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 12 && listA.Count == 0)
            {
                up = true;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 12) return;
            if (counter > 0.4) //If 0.4" have passed since last attack was thrown:
            {
                float yOffset = up ? -90 : 90;
                AddAttack(listA, new Vector2(160, Engine.center.y + yOffset), Vector2.right * 10);
                up = !up;
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x > 880);
        }
    }

    public class ThirdBoneAttack : BaseAttackPattern
    {
        public ThirdBoneAttack (Player player, Enemy enemy, IAbstractFactory factory) : base (player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 18 && listA.Count == 0)
            {
                selectPosition = 0;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 18) return;
            if (counter > 0.3)
            {
                float xPos = 200 + 125 * selectPosition;
                AddAttack(listA, new Vector2(xPos, Engine.center.y - 100), Vector2.down * 10);
                selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.y > 500);
        }
    }

    public class FourthBoneAttack : BaseAttackPattern
    {
        public FourthBoneAttack (Player player, Enemy enemy, IAbstractFactory factory) : base(player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 12 && listA.Count == 0)
            {
                up = true;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 12) return;
            if (counter > 0.4)
            {
                float yOffset = up ? -90 : 90;
                AddAttack(listA, new Vector2(880, Engine.center.y + yOffset), Vector2.left * 10);
                up = !up;
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x < 160);
        }
    }

    public class FifthBoneAttack : BaseAttackPattern
    {
        public FifthBoneAttack(Player player, Enemy enemy, IAbstractFactory factory) : base(player, enemy, factory) { }

        public override void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 18 && listA.Count == 0)
            {
                selectPosition = 0;
                EndIfFinished(true);
                return;
            }
            else if (numOfAttacks >= 18) return;
            if (counter > 0.3)
            {
                float xPos = 840 - 125 * selectPosition;
                AddAttack(listA, new Vector2(xPos, Engine.center.y + 100), Vector2.up * 10);
                selectPosition = (int)Helpers.Wrap(selectPosition + 1, 0, 7);
                counter = 0;
                numOfAttacks++;
            }
        }

        public override void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.y < 160);
        }
    }
}
