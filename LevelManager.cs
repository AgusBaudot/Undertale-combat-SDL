using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelManager
    {
        private Image fondo = Engine.LoadImage("assets/fondo.png");
        private Player player;
        private CombatArea combatArea;
        private Enemy enemy;
        private GameManager instance;

        #region FixedUpdate
        private float fixedDeltaTime = 0.02f;
        private float accumulatedTime = 0f;
        #endregion
        #region UI
        static private AttackButton attackButton;
        static private ActButton actButton;
        #endregion //Make UI class

        public LevelManager()
        {
            instance = GameManager.GetInstance();
            player = new Player(Engine.center);
            enemy = new Enemy(player, 560, 90);
            combatArea = new CombatArea();
            instance = GameManager.GetInstance();
            attackButton = new AttackButton(360, 600, enemy.healthController);
            actButton = new ActButton(720, 600, player.healthController);
        }

        public void Update()
        {
            accumulatedTime += Time.deltaTime;
            while (accumulatedTime >= fixedDeltaTime)
            {
                FixedUpdate();
                accumulatedTime -= fixedDeltaTime;
            }

            RegularUpdate();
            LateUpdate();
        }

        private void RegularUpdate()
        {
            GameState state = instance.GetGameState();
            enemy.Update(state);
            if (state == GameState.EnemyTurn)
            {
                player.Update();
            }
            if (state == GameState.PlayerTurn)
            {
                attackButton.Update();
                actButton.Update();
            }
        }

        private void FixedUpdate()
        {
            enemy.FixedUpdate();

            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                player.FixedUpdate();
            }
        }

        private void LateUpdate()
        {

        }

        public void Render()
        {
            Engine.Draw(fondo, 0, 0);
            combatArea.Render();
            if (instance.GetGameState() == GameState.EnemyTurn)
            {
                player.Render();
            }
            enemy.Render(instance.GetGameState());
            attackButton.Render();
            actButton.Render();
        }
    }
}
