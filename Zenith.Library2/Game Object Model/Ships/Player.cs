﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Player : Ship
    {
        private const double acceleration = 2000;

        public override void ShipLoop()
        {
            if (World.Instance.PlayerController.Up) AddForce(new Vector(0, -acceleration));
            if (World.Instance.PlayerController.Down) AddForce(new Vector(0, acceleration));
            if (World.Instance.PlayerController.Left) AddForce(new Vector(-acceleration, 0));
            if (World.Instance.PlayerController.Right) AddForce(new Vector(acceleration, 0));

            if (World.Instance.PlayerController.Fire) Shoot();
        }

        public Player(Vector position)
            : base(position)
        {
            type = GameObjectType.Player;
            imageSources = new string[] {
                Util.GetShipSpriteFolderPath("blue_01.png")
            };
            angle = 0;
            firePattern = new int[] { 0 };
            accuracy = 0.1;
            //size = new Vector(128, 128);
        }
    }
}
