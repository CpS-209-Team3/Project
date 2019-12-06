using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss3Cannon : Cannon
    {
        public Boss3Cannon(Ship host)
            : base(host)
        {
            firePattern = new List<int> { 300 };

            for (int i = 0; i < 100; ++i)
            {
                firePattern.Add(0);
            }
            damage = 20;
        }
    }
}
