using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.assets;

namespace MyGame
{
    public interface IAbstractFactory
    {
        BaseBoneAttack CreateBoneAttack(Vector2 position, Vector2 speed, BoxCollider playerCollider, HealthController playerHealth, PlayerController playerController, Enemy enemy);
    }
}
