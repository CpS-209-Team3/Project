using System;
using System.Collections.Generic;
using System.Text;

// Levels should speed up the rate at which enemies spawn. check
// Enemies should be varied based on level. Different types have different stats. check
// Enemies should have more hp and damage based on difficulty.
// Certain levels should have a greater probability of spawning different units. check
// 

namespace Zenith.Library
{
    public class LevelManager
    {
        private int level;
        private int difficulty;
        private int spawnRate;
        private int timeUntilNextSpawn;

        public void Spawn()
        {
            int r = World.Instance.Random.Next(0, 5) + level;
           
            double x = World.Instance.Width;
            double y = World.Instance.Random.NextDouble() * (World.Instance.Height - 32);
            var pos = new Vector(x, y);

            if (r < 9 && r > 0)
            {
                if (r < 3) World.Instance.AddObject(new Enemy1(pos));
                else if (r < 7) World.Instance.AddObject(new Enemy2(pos));
                else World.Instance.AddObject(new Enemy3(pos));
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

        public LevelManager(int difficulty, int level)
        {
            this.difficulty = difficulty;
            this.level = level;
            spawnRate = 100 - (30 * difficulty) - (20 * level);
            timeUntilNextSpawn = spawnRate;
        }
    }
}
