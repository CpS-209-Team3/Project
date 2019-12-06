using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave5 : Wave
    {
        public Wave5(int difficulty, int level)
        {
            waveCount = 0;
            for (int i = 0; i < difficulty + level; i++)
            {
                Vector startingPos = new Vector(World.Instance.Width, World.Instance.Random.Next(0, Convert.ToInt32(World.Instance.Height)), false);
                double size = World.Instance.Random.NextDouble() * 20 + 30;
                Enemy1 e = new Enemy1(startingPos);
                e.OnDeath = DecreaseCount;
                Asteroid a = new Asteroid(startingPos, size);
                a.OnDeath = DecreaseCount;
                World.Instance.AddObject(e);
                waveCount++;
            }
        }
    }
}
