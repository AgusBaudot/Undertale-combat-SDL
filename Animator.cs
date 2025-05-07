using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Animator
    {
        #region Classes
        private SpriteRenderer renderer;
        #endregion
        #region Events
        public event Action OnAnimationEnd;
        #endregion
        #region Internal variables
        private List<Image> spriteSheet = new List<Image>();
        private float speed = 0f; //Speed is the amount of time that each frame will appear before changing to the next one.
        private float currentTime = 0f; //Counter of how much this sprite has been shown.
        private int spriteIndex = 0;
        private bool isLoop = false;
        #endregion

        public Animator(List<Image> images, float speed, SpriteRenderer renderer, bool loopeable) //Animator constructor.
        {
            foreach (Image sprite in images)
            {
                spriteSheet.Add(sprite); //Set local list with given list.
            }
            this.speed = speed; //Set speed of this animation.
            this.renderer = renderer; //Set SpriteRenderer of used by this animator.
            isLoop = loopeable; //Set whether this animation will be looped.
        }

        public void ChangeAnimation(List<Image> images, float speed, bool loopeable) //Change animation played.
        {
            spriteSheet.Clear(); //Clear current spritesheet.
            foreach (Image sprite in images) 
            {
                spriteSheet.Add(sprite); //Update it with new animation sheet.
            }
            this.speed = speed; //Update new speed
            isLoop = loopeable; //Update loop boolean.
        }

        public void Update()
        {
            currentTime += Time.deltaTime; //Update counter.
            if (currentTime >= speed) //If timer is up:
            {
                currentTime = 0; //Reset timer.
                spriteIndex++; //Change sprite to next one in list.
                if (spriteIndex >= spriteSheet.Count) //If end of list is reached call AnimationEnd method.
                {
                    AnimationEnd();
                }
            }
            Render(); //Render current sprite.
        }

        private void Render() //Render current sprite.
        {
            if (spriteSheet[spriteIndex].path != renderer.sprite.path) //Check if loaded sprite is different from local list sprite to avoid loading unnecesary images.
            {
                renderer.UpdateSprite(spriteSheet[spriteIndex].path); //Update sprite to match current index in list.
            }
            renderer.Render(); //Render selected sprite.
        }

        private void AnimationEnd() //Called when animation ends.
        {
            if (isLoop) spriteIndex = 0; //If animation is looped: reset index back to zero.
            OnAnimationEnd?.Invoke(); //Fire event when animation ends.

            //Event calles for a couple of frame since it has "speed" time until it changes.
        }
    }
}
