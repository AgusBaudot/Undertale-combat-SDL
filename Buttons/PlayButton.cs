namespace MyGame
{
    public class PlayButton : IButton
    {
        private Transform transform;
        private bool leftButton = true;
        private string normalSprite, selectedSprite;
        private SpriteRenderer renderer;
        private GameManager instance;

        public PlayButton(float x, float y)
        {
            transform = new Transform(x, y);
            normalSprite = "assets/Sprites/StartButton.png";
            selectedSprite = "assets/Sprites/StartButtonPressed.png";
            renderer = new SpriteRenderer(transform, Engine.LoadImage(normalSprite));
            instance = GameManager.GetInstance();
        }

        public void Update()
        {
            Inputs();

            if (leftButton && Engine.GetKeyDown(Engine.KEY_ESP))
            {
                Pressed();
            }
        }

        public void Inputs()
        {

            if (Engine.GetKeyDown(Engine.KEY_A))
            {
                leftButton = !leftButton;
            }

            else if (Engine.GetKeyDown(Engine.KEY_D))
            {
                leftButton = !leftButton;
            }
        }

        public void Pressed()
        {
            instance.OnGameStateChanged(GameState.PlayerTurn);
        }

        public void Render()
        {
            renderer.UpdateSprite((leftButton) ? selectedSprite : normalSprite);
            renderer.Render();
        }
    }
}
