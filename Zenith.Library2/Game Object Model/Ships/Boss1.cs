using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss1 : Enemy
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
            imageSource = Util.GetShipSpriteFolderPath("large_grey_01.png");
            angle = Math.PI;
            type = GameObjectType.Boss1;
            size = new Vector(256, 256);
        }
    }
}
