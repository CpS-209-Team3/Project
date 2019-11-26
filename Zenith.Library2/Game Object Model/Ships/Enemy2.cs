using System;
using System.Collections.Generic;
using System.Text;


// needs a different sprite. 
// needs its own movemment pattern put in ship loop.
// 




namespace Zenith.Library
{
    class Enemy2 : Enemy
    {
        public override void ShipLoop() { }

        public Enemy2(Vector position)
           : base(position)
        {
            type = GameObjectType.Enemy2;
        }
    }
}
