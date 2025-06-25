using System;
using System.Collections.Generic;

namespace MyGame
{
    public interface IAttackPatterns
    {
        public event Action OnAttackEnd;
        public void SpawnAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition);
        public void UpdateAttack(List<BaseBoneAttack> attackList, ref float duration);
        public void RemoveAttack(List<BaseBoneAttack> listA, List<BaseBoneAttack> listB);
        public void RenderList(List<BaseBoneAttack> attackList, ref Tao.Sdl.Sdl.SDL_Rect clipRect);
    }
}
