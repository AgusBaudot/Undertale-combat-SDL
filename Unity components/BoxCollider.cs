namespace MyGame
{
    public class BoxCollider
    {
        private Transform transform;
        private SpriteRenderer renderer;

        public Vector2 max {  get; private set; }
        public Vector2 min {  get; private set; }
        public Vector2 center { get; private set; }

        public BoxCollider(Transform transform, SpriteRenderer renderer)
        {
            max = new Vector2 (transform.position.x + renderer.scaledWidth/2, transform.position.y + renderer.scaledHeight/2);
            min = new Vector2(transform.position.x - renderer.scaledWidth / 2, transform.position.y - renderer.scaledHeight / 2);
            center = transform.position;
            this.renderer = renderer;
            this.transform = transform;
        }

        public void Update ()
        {
            max = new Vector2(transform.position.x + renderer.scaledWidth / 2, transform.position.y + renderer.scaledHeight / 2);
            min = new Vector2(transform.position.x - renderer.scaledWidth / 2, transform.position.y - renderer.scaledHeight / 2);
            center = transform.position;
        }
    }
}
