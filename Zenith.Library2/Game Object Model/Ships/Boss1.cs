using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss1 : Enemy
    {
        public override void ShipLoop()
        {
            switch (state)
            {
                case EnemyState.Sway:

                    break;
                case EnemyState.Flee:

                    break;


            }
        }

        public Boss1(Vector position)
            : base(position)
        {
            type = GameObjectType.Boss1;
        }
    }
}
