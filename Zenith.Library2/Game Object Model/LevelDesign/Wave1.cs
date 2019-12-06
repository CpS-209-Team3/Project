using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Wave1 : Wave
    {
        public Wave1(int difficulty, int level)
        {
            for (int i = 0; i < difficulty + level + 2; i++)
            {
                
                Asteroid a = new Asteroid(startingPos, size);
                AddEnemy(a);
            }
        }
    }
}
