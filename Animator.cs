using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{

    public class Animator
    {
        #region Variables
        private List<Image> spriteSheet = new List<Image>();
        private SpriteRenderer renderer;
        private float speed = 0f;
        private float currentTime = 0f;
        private int spriteIndex = 0;
        private bool isLoop = false;
        public event Action OnAnimationEnd;
        #endregion

        public Animator(List<Image> images, float speed, SpriteRenderer renderer, bool loopeable)
        {
            foreach (Image sprite in images)
            {
                spriteSheet.Add(sprite); //Load list with given list.
            }
            this.speed = speed; //Get speed of this animation.
            this.renderer = renderer;
            isLoop = loopeable;
        }

        public void ChangeAnimation(List<Image> images, float speed, bool loopeable) //Change animation played.
        {
            spriteSheet.Clear(); //Clear current spritesheet.
            foreach (Image sprite in images) //Update it with new animation sheet.
            {
                spriteSheet.Add(sprite);
            }
            this.speed = speed; //Update new speed
            isLoop = loopeable;
        }

        public void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= speed)
            {
                currentTime = 0;
                spriteIndex++;
                if (spriteIndex >= spriteSheet.Count)
                {
                    AnimationEnd();
                }
            }
            Render();
        }

        private void Render()
        {
            if (spriteSheet[spriteIndex].path != renderer.sprite.path) //Check if loaded sprite is different from local list sprite to avoid loading unnecesary images.
            {
                renderer.UpdateSprite(spriteSheet[spriteIndex].path);
            }
            renderer.Render();
        }

        private void AnimationEnd()
        {
            if (isLoop) spriteIndex = 0;
            OnAnimationEnd?.Invoke(); //Fire event when animation ends.
            //Event calles for a couple of frame since it has "speed" time until it changes.
        }
    }
}
