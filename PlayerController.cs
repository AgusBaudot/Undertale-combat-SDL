using System;
using System.Collections.Generic;
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
        private int speed = 3;
        private Vector2 input;
        private Vector2 playerScale;

        public PlayerController(Transform transform, float w, float h)
        {
            this.transform = transform;
            this.moveArea = new CombatArea();
            playerScale = new Vector2(w, h);
        }
        public void Inputs()
        {
            #region Input movement
            input = Vector2.zero;
            if (Engine.GetKey(Engine.KEY_A))
            {
                //transform.Translate(Vector2.left * speed);
                input += Vector2.left;
            }

            if (Engine.GetKey(Engine.KEY_D))
            {
                //transform.Translate(Vector2.right * speed);
                input += Vector2.right;
            }

            if (Engine.GetKey(Engine.KEY_W))
            {
                //transform.Translate(Vector2.up * speed);
                input += Vector2.up;
            }

            if (Engine.GetKey(Engine.KEY_S))
            {
                //transform.Translate(Vector2.down * speed);
                input += Vector2.down;
            }
            #endregion
            Move();
        }

        private void Move()
        {
            transform.Translate(input.normalized * speed);
            Limits();
        }

        private void Limits()
        {
            if (transform.position.x + playerScale.x >= moveArea.transform.position.x + moveArea.scale.x/2) 
            {
                transform.position = new Vector2 (moveArea.transform.position.x + moveArea.scale.x/2 - playerScale.x , transform.position.y);
            }
            if (transform.position.x - playerScale.x <= moveArea.transform.position.x - moveArea.scale.x/2)
            {
                transform.position = new Vector2(moveArea.transform.position.x - moveArea.scale.x/2 + playerScale.x , transform.position.y);
            }
            if (transform.position.y + playerScale.y >= moveArea.transform.position.y + moveArea.scale.y/2)
            {
                transform.position = new Vector2(transform.position.x, moveArea.transform.position.y + moveArea.scale.y / 2 - playerScale.y);
            }
            if (transform.position.y - playerScale.y <= moveArea.transform.position.y - moveArea.scale.y/2)
            {
                transform.position = new Vector2(transform.position.x, moveArea.transform.position.y - moveArea.scale.y / 2 + playerScale.y);
            }
        }
    }
}
