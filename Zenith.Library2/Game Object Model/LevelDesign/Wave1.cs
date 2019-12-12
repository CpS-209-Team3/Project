//-----------------------------------------------------------
//File:   Wave1.cs
//Desc:   This class defines the enemies that will spawn in the first wave.
//----------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This class spawns enemies according the level and difficulty of the game
    public class Wave1 : Wave
    {

        public override void Spawn()
        {
            for (int i = 0; i < difficulty + (level * 2); i++)
            {

                startingPos = new Vector(World.Instance.Width, World.Instance.Random.Next(0, Convert.ToInt32(World.Instance.Height)), false);
                size = World.Instance.Random.NextDouble() * 100 + 30;

                Enemy1 e1 = new Enemy1(startingPos);
                Asteroid a = new Asteroid(startingPos, size);
                AddEnemy(e1);
                AddEnemy(a);
            }
        }

    }
}
