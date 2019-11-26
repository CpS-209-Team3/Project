using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Enemy1 : Enemy
    {
        public override void ShipLoop() {
            if (reloadTime == 0)
            {
                var p = World.Instance.Player.Position - position;
                direction = p.Angle;
                Shoot();
            }

           
        }

        public Enemy1(Vector position, int difficulty)
           : base(position)
        {
            type = GameObjectType.Enemy1;
            imageSource = Util.GetShipSpriteFolderPath("blue_01.png");
            imageRotation = 270;
            this.velocity.X = -50;

            // upgrade stats based on difficulty
            this.velocity.X -= difficulty * 10;
            this.Health += difficulty * 20;
            this.fireRate -= difficulty * 2;

        }
    }
}
