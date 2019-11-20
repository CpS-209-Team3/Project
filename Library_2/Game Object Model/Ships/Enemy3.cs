using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Enemy3 : Enemy
    {
        public override void Loop() { }

        public Enemy3(Vector position, Ship player)
            : base(position, player)
        {

        }
    }
}
