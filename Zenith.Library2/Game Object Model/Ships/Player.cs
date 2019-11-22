using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Player : Ship
    {
        private double acceleration = 2000;

        public override void ShipLoop()
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
            if (!isAccerlating) velocity *= 0.97;

            if (World.Instance.PlayerController.Fire) Shoot();
        }

        public Player(Vector position)
            : base(position)
        {
            imageSource = Util.GetShipSpriteFolderPath("blue_01.png");
            imageRotation = 90;
            isPlayer = true;
        }
    }
}
