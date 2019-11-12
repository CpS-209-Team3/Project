using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Game_Object_Model
{
    class SpawnManager
    {
        private int difficulty;

        public void Spawn()
        {
            int r = World.Random.Next(0, 100);
            double x = World.Random.NextDouble() * World.Width;

            if (r < 9)
            {
                r = World.Random.Next(0, 10);
                if (r < 3) World.AddObject(new Enemy1(new Vector(0, 0), World.Player));
                else if (r < 7) World.AddObject(new Enemy1(new Vector(0, 0), World.Player));
                else World.AddObject(new Enemy1(new Vector(0, 0), World.Player));
            }
            else
            {
                World.AddObject(new Asteroid(
                    new Vector(0, 0),
                    World.Random.Next(30, 300))
                    );
            }
        }

        public SpawnManager(int difficulty)
        {
            this.difficulty = difficulty;
        }
    }
}
