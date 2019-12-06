using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave5 : Wave
    {
        public Wave5(int difficulty, int level)
        {
            Boss1 b = new Boss1(new Vector(World.Instance.Width, World.Instance.Random.Next(0, Convert.ToInt32(World.Instance.Height)), false));
            AddEnemy(b);
        }
    }
}
