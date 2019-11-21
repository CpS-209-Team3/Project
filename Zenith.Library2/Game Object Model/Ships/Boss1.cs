using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss1 : Enemy
    {
        public override void ShipLoop() { }

        public Boss1(Vector position)
            : base(position) { }
    }
}
