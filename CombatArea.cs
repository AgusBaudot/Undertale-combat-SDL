namespace MyGame
{
    public class CombatArea
    {
        public Transform bgTransform { get; private set; }
        private Transform borderTransform;
        private SpriteRenderer bgRenderer;
        private SpriteRenderer borderRenderer;
        private Image boxSprite = Engine.LoadImage("assets/combatArea.png");
        private Image edgesSprite = Engine.LoadImage("assets/combatAreaBack.png");

        public CombatArea()
        {
            bgTransform = new Transform(Engine.center);
            borderTransform = new Transform(Engine.center);
            bgRenderer = new SpriteRenderer (bgTransform, boxSprite);
            borderRenderer = new SpriteRenderer(borderTransform, edgesSprite);
        }

        public Vector2 GetAreaLimits() => new Vector2(boxSprite.width, boxSprite.height);

        public void Render()
        {
            borderRenderer.Render();
            bgRenderer.Render();
        }
    }    
}
