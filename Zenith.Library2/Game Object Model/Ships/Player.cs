using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Player : Ship
    {
        private double acceleration = 0.02;

        public override void Loop()
        {
            bool isAccerlating = false;
            if (World.Instance.PlayerController.Up)
            {
                AddForce(new Vector(0, -acceleration));
                isAccerlating = true;
            }
            if (World.Instance.PlayerController.Down)
            {
                AddForce(new Vector(0, acceleration));
                isAccerlating = true;
            }
            if (World.Instance.PlayerController.Left)
            {
                AddForce(new Vector(-acceleration, 0));
                isAccerlating = true;
            }
            if (World.Instance.PlayerController.Right)
            {
                AddForce(new Vector(acceleration, 0));
                isAccerlating = true;
            }
            if (!isAccerlating) velocity *= 0.9999;

            if (World.Instance.PlayerController.Fire) Shoot(Math.PI / 2);
        }

        public Player(Vector position)
            : base(position)
        {
            maxSpeed = 5;
            imageSource = Util.GetImagePath("blue_01.png");
        }
    }
}
