using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public interface IAttackPatterns
    {
        public event Action OnAttackEnd;
        public void SpawnAttack(List<EnemyAttack> listA, List<EnemyAttack> listB, ref float counter, ref float duration, ref int numOfAttacks, ref bool up, ref int selectPosition);
        public void UpdateAttack(List<EnemyAttack> attackList, ref float duration);
        public void RemoveAttack(List<EnemyAttack> listA, List<EnemyAttack> listB);
        public void RenderList(List<EnemyAttack> attackList, ref Tao.Sdl.Sdl.SDL_Rect clipRect);
    }
}
