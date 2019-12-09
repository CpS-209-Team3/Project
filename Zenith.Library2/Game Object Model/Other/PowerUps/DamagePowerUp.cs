using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public abstract class DamagePowerUp : PowerUp
    {
        public DamagePowerUp(Vector position)
            : base(position)
        {
            Damage = 50;
        }
    }
}
