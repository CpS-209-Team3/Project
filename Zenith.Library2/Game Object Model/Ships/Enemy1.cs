using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Enemy1 : Enemy
    {
        public override void ShipLoop() {
            this.velocity.X = 0.01;
        }

        public Enemy1(Vector position)
           : base(position)
        {
            type = GameObjectType.Enemy1;
            imageSource = Util.GetImagePath("blue_01.png");
        }
    }
}
