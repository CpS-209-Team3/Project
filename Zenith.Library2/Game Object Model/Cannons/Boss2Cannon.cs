using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss2Cannon : Cannon
    {
        public Boss2Cannon(GameObject host)
            : base(host)
        {
            firePattern = new List<int> { 15, 15, 15, 100 };
            damage = 200;
        }
    }
}
