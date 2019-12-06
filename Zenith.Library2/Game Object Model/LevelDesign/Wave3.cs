using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave3 : Wave
    {
        public Wave3(int difficulty, int level)
        {
            waveCount = 0;
            for (int i = 0; i < difficulty + level; i++)
            {
                Enemy1 e1 = new Enemy1(startingPos);
                Asteroid a = new Asteroid(startingPos, size);
                Enemy2 e2 = new Enemy2(startingPos);
                AddEnemy(e1);
                AddEnemy(e2);
                AddEnemy(a);
            }
        }
    }
}
