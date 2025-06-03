using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.assets;

namespace MyGame
{
    public class WhiteBoneFactory : IAbstractFactory
    {
        public BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            return new WhiteBoneAttack(position, speed, playerCollider, playerHealth, enemy);
        }
    }

    public class BlueBoneFactory : IAbstractFactory
    {
        public BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy)
        {
            return new BlueBoneAttack(position, speed, playerCollider, playerHealth, playerController, enemy);
        }
    }
}
