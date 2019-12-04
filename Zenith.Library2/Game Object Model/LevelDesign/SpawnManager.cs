using System;
using System.Collections.Generic;
using System.Text;

// Not sure if I should rename the file, seeing as ive renamed the class.

// Waves. Im gonna use the ships ondeath variable to call a different method depending on what ship it is in the wave. Or maybe not.
// Lets first design level 1. We'll have 3 waves until the boss. Each Wave will spawn an assortment of different enemies. 

    // Basically the waves will spawn and the player will have to destroy them all to progress to the next wave. Might have a couple different wave classes
    // Final wave includes a boss, who has the power to spawn different waves.

    // Waves will not be Random. This will allow high score to be totally dependent on time it took to finish and level of difficulty.




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
        private Vector startingPos;
        private static int nextWave;

        public static int NextWave { get { return nextWave; } set { nextWave = value; } }

        public void Spawn()
        {
            new Wave1(difficulty, level);
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

            spawnRate = 100;
            timeUntilNextSpawn = spawnRate;
            

        }

        
    }
}
