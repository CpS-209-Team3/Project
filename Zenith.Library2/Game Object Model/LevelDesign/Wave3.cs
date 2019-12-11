using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave3 : Wave
    {
        public override void Spawn()
        {
            waveCount = 0;
            for (int i = 0; i < difficulty + (level * 2); i++)
            {
                startingPos = new Vector(World.Instance.Width, World.Instance.Random.Next(0, Convert.ToInt32(World.Instance.Height)), false);
                size = World.Instance.Random.NextDouble() * 100 + 30;
                
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
