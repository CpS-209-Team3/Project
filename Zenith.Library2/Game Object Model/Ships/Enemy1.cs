using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Enemy1 : Enemy
    {
        public override void Loop() { }

        public Enemy1(Vector position, Ship player)
           : base(position, player)
        {

        }
    }
}
