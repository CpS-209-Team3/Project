using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss1Cannon : Cannon
    {

        public Boss1Cannon(Ship host)
            : base(host)
        {
            firePattern = new List<int> { 15, 15, 100 };
            damage = 100;
            ProjectileColor = ProjectileColor.Red;
        }
    }
}
