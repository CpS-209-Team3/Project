using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss5 : Enemy
    {
        public override void ShipLoop() { }

        public Boss5(Vector position)
            : base(position) 
        {
            type = GameObjectType.Boss5;
        }
    }
}
