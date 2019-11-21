using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss2 : Enemy
    {
        public override void Loop() { }

        public Boss2(Vector position)
            : base(position) 
        {
            type = GameObjectType.Boss2;
        }
    }
}
