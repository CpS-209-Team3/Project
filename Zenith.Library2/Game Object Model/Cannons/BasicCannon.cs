using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class BasicCannon : Cannon
    {
        public BasicCannon(GameObject host, int fireRate)
            : base(host)
        {
            firePattern = new List<int> { fireRate };
        }
    }
}
