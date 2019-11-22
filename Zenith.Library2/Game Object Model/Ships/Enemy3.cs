using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Enemy3 : Enemy
    {
        public override void ShipLoop() { }

        public Enemy3(Vector position)
            : base(position)
        {
            type = GameObjectType.Enemy3;
        }
    }
}
