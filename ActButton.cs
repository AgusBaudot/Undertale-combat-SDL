using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ActButton : Button
    {
        #region Classes
        private SpriteRenderer spriteRenderer;
        private HealthController playerHealth;
        private GameManager instance;
        #endregion
        #region Internal variables
        private int heal = 20;
        private string normalSpritePath, selectedSpritePath;
        #endregion

        public ActButton(float x, float y, HealthController playerHealth) //ActButton constructor.
        {
            transform = new Transform(x, y); //Set button's transform with given position.
            normalSpritePath = "assets/Sprites/ActButton.png"; //Set normal sprite for this button.
            selectedSpritePath = "assets/Sprites/ActButtonPressed.png"; //Set selected sprite for this button.
            spriteRenderer = new SpriteRenderer(transform, Engine.LoadImage(normalSpritePath)); //Set SpriteRenderer and initialize it with normal sprite by default.
            this.playerHealth = playerHealth; //Set playerHealth to given value.
            instance = GameManager.GetInstance(); //Get instance of GameManager.
        }

        public override void Update()
        {
            base.Update(); //Call Button's Update.

            if (!leftButton && Engine.GetKeyDown(Engine.KEY_ESP)) //If left button isn't selected and player presses space:
            {
                Pressed(); //Call pressed method.
            }
        }

        private void Pressed() //Pressed method:
        {
            playerHealth.Recover(heal); //Act button heals player heal amount.
            instance.OnGameStateChanged(GameState.EnemyTurn); //Since player acted, change turns.
        }

        public void Render() //Render this button:
        {
            spriteRenderer.UpdateSprite((!leftButton) ? selectedSpritePath : normalSpritePath); //Update this sprite according to which button is currently selected.
            spriteRenderer.Render(); //Render updated sprite.
        }
    }
}
