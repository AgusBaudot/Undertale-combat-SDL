using System.Linq;
using System.Collections.Generic;
using Tao.Sdl;
using System;

namespace MyGame
{
    public class FirstBoneAttack : IAttackPatterns //Attack for spawning left and right attacks at the same time.
    {
        private Player player;
        private Enemy enemy;

        public FirstBoneAttack(Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void SpawnAttack(List<EnemyAttack> listA, List<EnemyAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (numOfAttacks >= 16) return;
            if (counter > 1.2f - (duration / 20)) //If 1 - (duration/20)" have passed since last attack was thrown:
            {
                AddAttack(listA, new Vector2(160, Engine.center.y + 90), Vector2.right * 5);
                AddAttack(listB, new Vector2(880, Engine.center.y - 90), Vector2.left * 5);
                counter = 0; //Reset attack timer.
                numOfAttacks += 2;
            }
        }

        public void UpdateAttack(List<EnemyAttack> attackList, ref float duration)
        {
            foreach (EnemyAttack attack in attackList)
            {
                Vector2 currentDir = attack.speed.normalized;
                attack.UpdateSpeed(currentDir * (3 + duration));
                attack.Update();
            }
        }

        public void RemoveAttack(List<EnemyAttack> listA, List<EnemyAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x > 880);
            listB.RemoveAll(a => a.transform.position.x < 160);
        }

        public void RenderList(List<EnemyAttack> attackList, ref Sdl.SDL_Rect clipRect)
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

        private void AddAttack(List<EnemyAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new EnemyAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class SecondBoneAttack : IAttackPatterns
    {

        private Player player;
        private Enemy enemy;

        public SecondBoneAttack(Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void SpawnAttack(List<EnemyAttack> listA, List<EnemyAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            if (counter > 0.4) //If 0.4" have passed since last attack was thrown:
            {
                float yOffset = up ? -90 : 90;
                AddAttack(listA, new Vector2(160, Engine.center.y + yOffset), Vector2.right * 10);
                up = !up;
                counter = 0;
            }
        }

        public void UpdateAttack(List<EnemyAttack> attackList, ref float duration)
        {
            attackList.ForEach(a => a.Update());
        }

        public void RemoveAttack(List<EnemyAttack> listA, List<EnemyAttack> listB)
        {
            listA.RemoveAll(a => a.transform.position.x > 880);
        }

        public void RenderList(List<EnemyAttack> attackList , ref Sdl.SDL_Rect clipRect)
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

        private void AddAttack(List<EnemyAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new EnemyAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class ThirdBoneAttack : IAttackPatterns
    {
        private Player player;
        private Enemy enemy;

        public ThirdBoneAttack(Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void RemoveAttack(List<EnemyAttack> listA, List<EnemyAttack> listB)
        {
            throw new System.NotImplementedException();
        }

        public void RenderList(List<EnemyAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            throw new System.NotImplementedException();
        }

        public void SpawnAttack(List<EnemyAttack> listA, List<EnemyAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAttack(List<EnemyAttack> attackList, ref float duration)
        {
            throw new System.NotImplementedException();
        }

        private void AddAttack(List<EnemyAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new EnemyAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class FourthBoneAttack : IAttackPatterns
    {
        private Player player;
        private Enemy enemy;

        public FourthBoneAttack(Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void RemoveAttack(List<EnemyAttack> listA, List<EnemyAttack> listB)
        {
            throw new System.NotImplementedException();
        }

        public void RenderList(List<EnemyAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            throw new System.NotImplementedException();
        }

        public void SpawnAttack(List<EnemyAttack> listA, List<EnemyAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAttack(List<EnemyAttack> attackList, ref float duration)
        {
            throw new System.NotImplementedException();
        }

        private void AddAttack(List<EnemyAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new EnemyAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    public class FifthBoneAttack : IAttackPatterns
    {
        private Player player;
        private Enemy enemy;

        public FifthBoneAttack(Player player, Enemy enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void RemoveAttack(List<EnemyAttack> listA, List<EnemyAttack> listB)
        {
            throw new System.NotImplementedException();
        }

        public void RenderList(List<EnemyAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            throw new System.NotImplementedException();
        }

        public void SpawnAttack(List<EnemyAttack> listA, List<EnemyAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAttack(List<EnemyAttack> attackList, ref float duration)
        {
            throw new System.NotImplementedException();
        }

        private void AddAttack(List<EnemyAttack> list, Vector2 position, Vector2 direction)
        {
            list.Add(new EnemyAttack(position, direction, player.GetCollider(), player.healthController, enemy));
        }
    }

    //More attacks added later. (Lasers).
}
