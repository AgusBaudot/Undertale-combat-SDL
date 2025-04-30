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
        public int health { get; private set; }
        private int maxHealth;
        public bool isInvencible { get; private set; }

        private float invencibilityDuration;
        private float counter;

        public HealthController(int maxHealth, float invencibilityDuration)
        {
            health = this.maxHealth = maxHealth;
            this.invencibilityDuration = invencibilityDuration;
        }

        public void Update()
        {
            if (counter < invencibilityDuration)
            {
                counter += Time.deltaTime;
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
            if (health > 0)
            {
                Invencibility();
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

        private void Invencibility()
        {
            counter = 0;
            isInvencible = true;
        }

        public void Reset()
        {
            health = maxHealth;
        }
    }
}
