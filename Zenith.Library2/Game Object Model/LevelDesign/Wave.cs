using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public abstract class Wave
    {
        protected int waveCount = 0;
        protected Vector startingPos;
        protected double size; 
        public int WaveCount { get { return waveCount; } set { waveCount = value; } }

        public void DeathAction()
        {
            waveCount--;
            if (waveCount == 0)
            {
                if (LevelManager.WaveNum < 5)
                {
                    LevelManager.WaveNum++;
                }
                else
                {
                    World.Instance.LevelManager.Level++;
                    World.Instance.LevelManager.CurrentWave = new Wave1(World.Instance.LevelManager.Difficulty, World.Instance.LevelManager.Level);
                }
            }
                
        }

        public void AddEnemy(Enemy type)
        {
            type.OnDeath = DeathAction;
            World.Instance.AddObject(type);
            waveCount++;
        }
    }
}
