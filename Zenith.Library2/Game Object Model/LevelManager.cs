//-----------------------------------------------------------
//File:   .cs
//Desc:   
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;


// Steps. Change level manager so that it spawns enemies one after another in waves. Change waves so that they're randomized.
// Change waves so that they hold a list of enemies. Add those enemies to current wave. Update the game background according to the level number.


// Two ways to change the level manager: When it gets initialized and when it gets updated. Lets focus on when it gets updated.
// Lets use Update.




namespace Zenith.Library
{
    // ???
    public class LevelManager
    {
        // ???
        private int level;

        // ???
        private int difficulty;

        // ???
        private int spawnRate;

        // ???
        private int timeUntilNextSpawn;

        // ???
        private Wave currentWave;

        // ???
        private static int waveNum;


        public int Level { get { return level; } set { level = value; } }
        public int Difficulty { get { return difficulty; } set { difficulty = value; } }
        public static int WaveNum { get { return waveNum; } set { waveNum = value; } }

        public Wave CurrentWave { get { return currentWave; } set { currentWave = value; } }

        // ???
        public void Update()
        {
            if (currentWave == null) currentWave = CreateWave(World.Instance.CurrentWave);
            if (World.Instance.EnemiesLeftInWave > 0) // doesnt work need it to active once
            {
                currentWave.WaveCount = World.Instance.EnemiesLeftInWave;
                World.Instance.EnemiesLeftInWave = 0;
            }
            if (currentWave.WaveCount == 0)
            {
                if (timeUntilNextSpawn > 0) --timeUntilNextSpawn;
                else
                {
                    currentWave = CreateWave(World.Instance.CurrentWave);
                    CurrentWave.Spawn();
                    timeUntilNextSpawn = spawnRate;
                }
            }

        }

        // ???
        public LevelManager()
        {
            spawnRate = 100; // time in between waves
            timeUntilNextSpawn = spawnRate;
        }

        // ???
        public Wave CreateWave(int nextWave)
        {
            switch (nextWave)
            {
                case 1:
                    return new Wave1();
                case 2:
                    return new Wave2();
                case 3:
                    return new Wave3();
                case 4:
                    return new Wave4();
                case 5:
                    return new Wave5();
            }
            return null;
        }

    }
}
