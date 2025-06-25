namespace MyGame
{
    public class WhiteBoneFactory : IAbstractFactory
    {
        private readonly ObjectPool<WhiteBoneAttack> pool = new ObjectPool<WhiteBoneAttack>();

        public BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            var attack = pool.Get();
            attack.Setup(position, speed, playerCollider, playerHealth, enemy);
            return attack;
        }

        public void ReturnAttack(BaseBoneAttack attack)
        {
            if (attack is WhiteBoneAttack white)
                pool.Return(white);
        }
    }

    public class BlueBoneFactory : IAbstractFactory
    {
        private readonly ObjectPool<BlueBoneAttack> pool = new ObjectPool<BlueBoneAttack>();

        public BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            var attack = pool.Get();
            attack.Setup(position, speed, playerCollider, playerHealth, playerController, enemy);
            return attack;
        }

        public void ReturnAttack(BaseBoneAttack attack)
        {
            if (attack is BlueBoneAttack blue)
                pool.Return(blue);
        }
    }
    
    public class OrangeBoneFactory : IAbstractFactory
    {
        private readonly ObjectPool<OrangeBoneAttack> pool = new ObjectPool<OrangeBoneAttack>();

        public BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            var attack = pool.Get();
            attack.Setup(position, speed, playerCollider, playerHealth, playerController, enemy);
            return attack;
        }

        public void ReturnAttack(BaseBoneAttack attack)
        {
            if (attack is OrangeBoneAttack orange)
                pool.Return(orange);
        }
    }
}
