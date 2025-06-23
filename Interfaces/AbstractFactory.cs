namespace MyGame
{
    public interface IAbstractFactory
    {
        BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy);

        void ReturnAttack(BaseBoneAttack attack);
    }
}
