using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy
    {
        private Vector2 position;
        private int health = 50;
        private int maxHealth = 50;
        private Player player;
        private Transform transform;
        private Image enemySprite = Engine.LoadImage("assets/Enemy.png");
        private SpriteRenderer renderer;
        public HealthController healthController;
        private List<EnemyAttack> attackListRight = new List<EnemyAttack>();
        private List<EnemyAttack> attackListLeft = new List<EnemyAttack>();

        private GameManager instance;
        private float counter = 0;

        public Enemy(Player player, float x, float y)
        {
            this.player = player;
            position = new Vector2(x, y);
            transform = new Transform(position);
            renderer = new SpriteRenderer (transform, enemySprite);
            healthController = new HealthController(health, maxHealth, 0.5f);
            instance = GameManager.GetInstance();
        }

        public void Update()
        {
            healthController.Update();

            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                counter += Program.deltaTime;

                if (counter > 1)
                {
                    attackListRight.Add(new EnemyAttack(new Vector2(160, Engine.center.y + 100), Vector2.right * 5, player.GetCollider(), player.healthController));
                    attackListLeft.Add(new EnemyAttack(new Vector2(900, Engine.center.y + -100), Vector2.left * 5, player.GetCollider(), player.healthController));
                    counter = 0;
                }

                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Update();
                    attackListLeft[i].Update();
                }
            }

        }

        public void Render()
        {
            renderer.Render();

            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                for (int i = 0; i < attackListRight.Count; i++)
                {
                    attackListRight[i].Render();
                    attackListLeft[i].Render();
                }
            }
            
        }

    }
}
