using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public abstract class BaseWhiteAttackPattern : IAttackPatterns
    {
        protected Player player;
        protected Enemy enemy;
        protected IAbstractFactory factory;

        public event Action OnAttackEnd;

        protected BaseWhiteAttackPattern(Player player, Enemy enemy, IAbstractFactory factory)
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

    public abstract class BaseBlueAttackPattern : IAttackPatterns
    {
        protected Player player;
        protected Enemy enemy;
        protected IAbstractFactory mainFactory;
        protected IAbstractFactory secondaryFactory;

        public event Action OnAttackEnd;

        protected BaseBlueAttackPattern(Player player, Enemy enemy, IAbstractFactory factory, IAbstractFactory factory2)
        {
            this.player = player;
            this.enemy = enemy;
            mainFactory = factory;
            secondaryFactory = factory2;
        }

        protected void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction, IAbstractFactory factoryToUse)
        {
            list.Add(factoryToUse.CreateBoneAttack(position, direction, player.GetCollider(), player.healthController, player.GetPlayerController(), enemy));
        }

        protected void RenderAttacks(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
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
}
