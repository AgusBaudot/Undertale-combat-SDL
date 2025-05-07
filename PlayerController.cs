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
        #region CombatArea
        private Vector2 areaCenter;
        private Vector2 areaHalfSize;
        private float minX, maxX, minY, maxY;
        #endregion

        public PlayerController(Transform transform, float w, float h)
        {
            this.transform = transform;
            moveArea = new CombatArea();
            playerSize = new Vector2(w, h);
            SetAreaMovement();
        }
        public void Inputs()
        {
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
            float clampedX = Helpers.Clamp(transform.position.x, minX, maxX);
            float clampedY = Helpers.Clamp(transform.position.y, minY, maxY);
            transform.position = new Vector2(clampedX, clampedY);
        }

        private void SetAreaMovement()
        {
            areaCenter = moveArea.bgTransform.position;
            areaHalfSize = moveArea.GetAreaLimits() / 2;

            minX = areaCenter.x - areaHalfSize.x + playerSize.x / 2;
            maxX = areaCenter.x + areaHalfSize.x - playerSize.x / 2;
            minY = areaCenter.y - areaHalfSize.y + playerSize.y / 2;
            maxY = areaCenter.y + areaHalfSize.y - playerSize.y / 2;
        }
    }
}
