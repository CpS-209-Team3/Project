using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave2 : Wave
    {
        public Wave2(int difficulty, int level)
        {
            for (int i = 0; i < difficulty + level; i++)
            {
                Enemy1 e1 = new Enemy1(startingPos);
                Asteroid a = new Asteroid(startingPos, size);
                AddEnemy(e1);
                AddEnemy(a);
            }
        }
    }
}
