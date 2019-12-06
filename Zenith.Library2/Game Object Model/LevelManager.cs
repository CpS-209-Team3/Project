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
    public enum WaveType
    {
        Wave1,
        Wave2,
        Wave3,
        Wave4,
        Wave5
    }
    public class LevelManager
    {
        private int level;
        private int difficulty;
        private int spawnRate;
        private int timeUntilNextSpawn;
        private Wave currentWave;
        private static int waveNum;

        public static int WaveNum { get { return waveNum; } set { waveNum = value; } }

        public Wave CurrentWave { get { return currentWave; } set { currentWave = value; } }

        public void Spawn()
        {
            currentWave = SpawnWave(WaveNum);
        }

        public void Update()
        {
            if (currentWave == null) Spawn();
            if (currentWave.WaveCount == 0)
            {
                if (timeUntilNextSpawn > 0) --timeUntilNextSpawn;
                else
                {
                    Spawn();
                    timeUntilNextSpawn = spawnRate;
                }
            }
            
        }

        public LevelManager(int difficulty, int level)
        {
            this.difficulty = difficulty;
            this.level = level;
            waveNum = 1;
            spawnRate = 100;
            timeUntilNextSpawn = spawnRate;
        }

        public Wave SpawnWave(int nextWave)
        {
            switch(nextWave)
            {
                case 1:
                    return new Wave1(difficulty, level);
                case 2:
                    return new Wave2(difficulty, level);
                case 3:
                    return new Wave3(difficulty, level);
                case 4:
                    return new Wave4(difficulty, level);
                case 5:
                    return new Wave5(difficulty, level);
            }
            return null;
        }
        
    }
}
