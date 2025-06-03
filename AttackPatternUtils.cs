using System.Collections.Generic;
using Tao.Sdl;

namespace MyGame
{
    public static class AttackPatternUtils //Favored composition over inheritance to have variety in the project (Inheritance is BaseBoneAttack).
    {
        public static void RenderList(List<BaseBoneAttack> attackList, ref Sdl.SDL_Rect clipRect)
        {
            Sdl.SDL_SetClipRect(Engine.screen, ref clipRect);
            foreach (var attack in attackList)
            {
                attack.Render();
            }
            Sdl.SDL_Rect screenRect = new Sdl.SDL_Rect(0, 0, (short)Engine.width, (short)Engine.height);
            Sdl.SDL_SetClipRect(Engine.screen, ref screenRect);
        }

        public static void UpdateBasic(List<BaseBoneAttack> attackList)
        {
            attackList.ForEach(a => a.Update());
        }

        public static void AddAttack(List<BaseBoneAttack> list, Vector2 position, Vector2 direction, IAbstractFactory factory, Player player, Enemy enemy)
        {
            list.Add(factory.CreateBoneAttack(position, direction, player.GetCollider(), player.healthController, player.GetPlayerController(), enemy));
        }
    }

}
