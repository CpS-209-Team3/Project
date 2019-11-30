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
                Shoot();
            }
        }

        public Enemy1(Vector position)
           : base(position)
        {
            type = GameObjectType.Enemy1;
            imageSource = Util.GetShipSpriteFolderPath("blue_01.png");
            angle = Math.PI;
            velocity.X = -50;
            this.position.X = World.Instance.Width;
            fireRate = 300;
            swayRadius = 20;
        }
    }
}
