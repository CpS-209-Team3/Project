using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Wave1 : Wave
    {

        public override void Spawn()
        {
            for (int i = 0; i < difficulty + (level * 2); i++)
            {

                startingPos = new Vector(World.Instance.Width, World.Instance.Random.Next(0, Convert.ToInt32(World.Instance.Height)), false);
                size = World.Instance.Random.NextDouble() * 100 + 30;
                Asteroid a = new Asteroid(startingPos, size);
                AddEnemy(a);
            }
        }

    }
}
