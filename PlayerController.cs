using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public class PlayerController
    {
        private Transform transform;
        private int speed = 3;
        private Vector2 input;

        public PlayerController(Transform transform)
        {
            this.transform = transform;
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
        }
    }
}
