using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Enemy3 : Enemy
    {
        public override void ShipLoop()
        {
            var playerOffset = World.Instance.Player.Position - position;
            Shoot();

            if (playerOffset.Magnitude > 200)
            {
                playerOffset.Magnitude = 500;
                AddForce(playerOffset);
            }
            else
            {
                playerOffset.Magnitude = 500;
                AddForce(playerOffset * -1);
            }
            angle = playerOffset.Angle;
        }

        public Enemy3(Vector position)
            : base(position)
        {
            type = GameObjectType.Enemy3;
            imageSources = new string[] { Util.GetShipSpriteFolderPath("purple_06.png") };
            imageRotation = 90;
            angle = Math.PI;
            firePattern = new int[] { 120 };
        }
    }
}
