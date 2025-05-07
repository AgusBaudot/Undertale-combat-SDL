using System.Collections.Generic;

namespace MyGame
{
    public class Enemy
    {
        private Vector2 position;
        private int maxHealth = 50;
        private Transform transform;
        private SpriteRenderer renderer;
        public HealthController healthController;
        private AttackHandler attackHandler;
        private Animator anim;

        private GameManager instance;

        public Enemy(Player player, float x, float y, CombatArea combatArea)
        {
            position = new Vector2(x, y);
            transform = new Transform(position);
            healthController = new HealthController(maxHealth, 0.5f);
            List<Image> spriteSheet = new List<Image>();
            for (int i = 1; i <= 8; i++)
            {
                spriteSheet.Add(Engine.LoadImage($"assets/Spritesheet_{i}.png"));
            }
            renderer = new SpriteRenderer (transform, spriteSheet[0]);
            anim = new Animator(spriteSheet, 0.15f, renderer, true);
            attackHandler = new AttackHandler(player, this, combatArea);

            instance = GameManager.GetInstance();
        }

        public void Update(GameState currentState)
        {
            IsAlive();

            healthController.Update();
            if (currentState == GameState.EnemyTurn)
            {
                
                attackHandler.Update();
            }
        }

        public void FixedUpdate()
        {
            attackHandler?.FixedUpdate();
        }

        public void Render(GameState state)
        {
            anim.Update();
            anim.OnAnimationEnd += AnimationEnded;
            if (state == GameState.EnemyTurn)
            {
                attackHandler.Render();
            }
        }

        private void AnimationEnded()
        {
            //Animation has ended.
        }

        private void IsAlive()
        {
            if (healthController.health <= 0)
            {
                instance.OnGameStateChanged(GameState.Win);
            }
        }

        public void Reset()
        {
            healthController.Reset();
            attackHandler.Reset();
        }
    }
}
