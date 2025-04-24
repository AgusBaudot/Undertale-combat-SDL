using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class HealthController
    {
        private int health;
        private int maxHealth;
        public bool death {  get; private set; }
        public bool isInvencible { get; private set; }

        private float invencibilityDuration;
        private float counter;

        public HealthController(int health, int maxHealth, float invencibilityDuration)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.invencibilityDuration = invencibilityDuration;
        }

        public void Update()
        {
            if (counter < invencibilityDuration)
            {
                counter += Program.deltaTime;
            }
            else
            {
                isInvencible = false;
            }
        }

        public void TakeDamage(int damage)
        {
            if (isInvencible) return;

            health -= damage;

            if(health < 0)
            {
                health = 0;
            }
            if (health == 0)
            {
                death = true;
            }
            if (health > 0)
            {
                invecibility();
            }

            Engine.Debug(health.ToString());
        }

        public void Recover(int heal)
        {
            health += heal;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            Engine.Debug(health.ToString());
        }

        private void invecibility()
        {
            counter = 0;
            isInvencible = true;
        }
    }
}
