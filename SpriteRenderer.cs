using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public class SpriteRenderer
    {
        private Transform transform;
        private Image sprite;
        private float scaledWidth { get; /*private*/ set; }
        private float scaledHeight { get; /*private*/ set; }

        public SpriteRenderer(Transform transform, Image sprite)
        {
            this.transform = transform;
            this.sprite = sprite;
            scaledWidth = transform.scale.x * sprite.width;
            scaledHeight = transform.scale.y * sprite.height;
        }

        public void Render()
        {
            Sdl.SDL_Rect dstRect = new Sdl.SDL_Rect
            {
                x = (short)(transform.position.x - scaledWidth / 2),
                y = (short)(transform.position.y - scaledHeight / 2),
                w = (short)(scaledWidth),
                h = (short)(scaledHeight)
            }; //Create new rect for with image offset.
            Engine.Draw(sprite, transform.position.x, transform.position.y, dstRect); //Render image centered.
        }
    }
}
