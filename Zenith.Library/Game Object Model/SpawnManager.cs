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
            int r = World.Instance.Random.Next(0, 100);
            double x = World.Instance.Random.NextDouble() * World.Instance.Width;

            if (r < 9)
            {
                r = World.Instance.Random.Next(0, 10);
                if (r < 3) World.Instance.AddObject(new Enemy1(new Vector(0, 0), World.Instance.Player));
                else if (r < 7) World.Instance.AddObject(new Enemy1(new Vector(0, 0), World.Instance.Player));
                else World.Instance.AddObject(new Enemy1(new Vector(0, 0), World.Instance.Player));
            }
            else
            {
                World.Instance.AddObject(new Asteroid(
                    new Vector(0, 0),
                    World.Instance.Random.Next(30, 300))
                    );
            }
        }

        public SpawnManager(int difficulty)
        {
            this.difficulty = difficulty;
        }
    }
}
