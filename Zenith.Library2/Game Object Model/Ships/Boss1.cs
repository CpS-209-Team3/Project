using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss1 : Enemy
    {
        public override void Loop() { }

        public Boss1(Vector position)
            : base(position) 
        {
            type = GameObjectType.Boss1;
        }
    }
}
