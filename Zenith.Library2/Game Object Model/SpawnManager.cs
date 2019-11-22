using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class SpawnManager
    {
        private int difficulty;
        private int spawnRate;
        private int timeUntilNextSpawn;

        public void Spawn()
        {
            int r = World.Instance.Random.Next(0, 10);

            double x = World.Instance.Width;
            double y = World.Instance.Random.NextDouble() * (World.Instance.Height - 32);
            var pos = new Vector(x, y);

            if (r < 9)
            {
                if (r < 3) World.Instance.AddObject(new Enemy1(pos));
                else if (r < 7) World.Instance.AddObject(new Enemy1(pos));
                else World.Instance.AddObject(new Enemy1(pos));
            }
            else
            {
                World.Instance.AddObject(new Asteroid(
                    pos,
                    World.Instance.Random.Next(30, 50))
                    );
            }
        }

        public void Update()
        {
            if (timeUntilNextSpawn > 0) --timeUntilNextSpawn;
            else
            {
                Spawn();
                timeUntilNextSpawn = spawnRate;
            }
        }

        public SpawnManager(int difficulty)
        {
            this.difficulty = difficulty;
            spawnRate = 400 - 30 * difficulty;
            timeUntilNextSpawn = spawnRate;
        }
    }
}
