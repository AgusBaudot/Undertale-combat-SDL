using System.Linq;
using System.Collections.Generic;
using Tao.Sdl;
using System;
using System.Diagnostics;

namespace MyGame
{
    public class FirstBoneAttack : IAttackPatterns //Attack for spawning left and right attacks at the same time.
    {
        private Player player;
        private Enemy enemy;

        public event Action OnAttackEnd;

        public FirstBoneAttack(Player player, Enemy enemy, ConcreteFactory factory)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 16 && listA.Count == 0 && listB.Count == 0)
            {
                OnAttackEnd?.Invoke();
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

        public void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            foreach (BaseBoneAttack attack in attackList)
            {
                Vector2 currentDir = attack.speed.normalized;
                attack.UpdateSpeed(currentDir * (3 + duration));
                attack.Update();
            }
        }

        public void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x > 880);
            listB.RemoveAll(a => a.transform.position.x < 160);
        }

        public void RenderList(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect =
                new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        private void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new WhiteBoneAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class SecondBoneAttack : IAttackPatterns
    {

        private Player player;
        private Enemy enemy;

        public event Action OnAttackEnd;

        public SecondBoneAttack(Player player, Enemy enemy, ConcreteFactory factory)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 12 && listA.Count == 0)
            {
                up = true;
                OnAttackEnd?.Invoke();
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

        public void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            attackList.ForEach(a => a.Update());
        }

        public void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x > 880);
        }

        public void RenderList(List<BaseBoneAttack> attackList , ref Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect =
                new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        private void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new WhiteBoneAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class ThirdBoneAttack : IAttackPatterns
    {
        private Player player;
        private Enemy enemy;

        public event Action OnAttackEnd;

        public ThirdBoneAttack(Player player, Enemy enemy, ConcreteFactory factory)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 18 && listA.Count == 0)
            {
                selectPosition = 0;
                OnAttackEnd?.Invoke();
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

        public void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            attackList.ForEach(a => a.Update());
        }

        public void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.y > 500);
        }

        public void RenderList(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect =
                new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        private void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new WhiteBoneAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class FourthBoneAttack : IAttackPatterns
    {
        private Player player;
        private Enemy enemy;
        private ConcreteFactory factory;

        public event Action OnAttackEnd;

        public FourthBoneAttack(Player player, Enemy enemy, ConcreteFactory factory)
        {
            this.player = player;
            this.enemy = enemy;
            this.factory = factory;
        }

        public void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 12 && listA.Count == 0)
            {
                up = true;
                OnAttackEnd?.Invoke();
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

        public void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            attackList.ForEach(a => a.Update());
        }

        public void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x < 160);
        }

        public void RenderList(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect =
                new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        private void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new WhiteBoneAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class FifthBoneAttack : IAttackPatterns
    {
        private Player player;
        private Enemy enemy;
        private ConcreteFactory factory;

        public event Action OnAttackEnd;

        public FifthBoneAttack(Player player, Enemy enemy, ConcreteFactory factory)
        {
            this.player = player;
            this.enemy = enemy;
            this.factory = factory;
        }

        public void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 18 && listA.Count == 0)
            {
                selectPosition = 0;
                OnAttackEnd?.Invoke();
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

        public void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration)
        {
            attackList.ForEach(a => a.Update());
        }

        public void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.y < 160);
        }

        public void RenderList(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect =
                new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        private void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new WhiteBoneAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    //More attacks added later. (Lasers).
}
