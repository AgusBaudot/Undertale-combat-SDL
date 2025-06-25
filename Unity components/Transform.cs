namespace MyGame
{
    public class Transform
    {
        public Vector2 position { get; set; }
        public Vector2 scale { get; set; }

        private Vector2 lastFramePosition;

        public Transform(float x, float y)
        {
            position = new Vector2(x, y);
            lastFramePosition = position;
            scale = Vector2.one;
        }

        public Transform(Vector2 pos)
        {
            position = new Vector2(pos.x, pos.y);
            scale = Vector2.one;
        }

        public Transform(float x, float y, float w, float h)
        {
            position = new Vector2(x, y);
            scale = new Vector2(w, h);
        }

        public void Translate(Vector2 translation)
        {
            lastFramePosition = position;
            position += translation;
        }
    }
}
