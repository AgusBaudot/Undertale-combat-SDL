using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public class PlayerController
    {
        private Transform transform;
        private CombatArea moveArea;
        private int speed = 6;
        private Vector2 input;
        private Vector2 playerSize;
        private GameManager instance;

        public PlayerController(Transform transform, float w, float h)
        {
            this.transform = transform;
            moveArea = new CombatArea();
            playerSize = new Vector2(w, h);
        }
        public void Inputs()
        {
            instance = GameManager.GetInstance();

            #region Input movement
            input = Vector2.zero;
            if (Engine.GetKey(Engine.KEY_A))
            {
                input += Vector2.left;
            }

            if (Engine.GetKey(Engine.KEY_D))
            {
                input += Vector2.right;
            }

            if (Engine.GetKey(Engine.KEY_W))
            {
                input += Vector2.up;
            }

            if (Engine.GetKey(Engine.KEY_S))
            {
                input += Vector2.down;
            }
            #endregion
        }

        public void Move()
        {
            transform.Translate(input.normalized * speed);
            CalculateLimits();
        }

        private void CalculateLimits()
        {
            if (transform.position.x + playerSize.x/2 >= moveArea.bgTransform.position.x + moveArea.GetAreaLimits().x/2) 
            {
                transform.position = new Vector2 (moveArea.bgTransform.position.x + moveArea.GetAreaLimits().x/2 - playerSize.x/2 , transform.position.y);
            }
            if (transform.position.x - playerSize.x/2 <= moveArea.bgTransform.position.x - moveArea.GetAreaLimits().x/2)
            {
                transform.position = new Vector2(moveArea.bgTransform.position.x - moveArea.GetAreaLimits().x/2 + playerSize.x/2 , transform.position.y);
            }
            if (transform.position.y + playerSize.y/2 >= moveArea.bgTransform.position.y + moveArea.GetAreaLimits().y / 2)
            {
                transform.position = new Vector2(transform.position.x, moveArea.bgTransform.position.y + moveArea.GetAreaLimits().y / 2 - playerSize.y/2);
            }
            if (transform.position.y - playerSize.y/2 <= moveArea.bgTransform.position.y - moveArea.GetAreaLimits().y/2)
            {
                transform.position = new Vector2(transform.position.x, moveArea.bgTransform.position.y - moveArea.GetAreaLimits().y / 2 + playerSize.y/2);
            }
        }
    }
}
