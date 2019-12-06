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
                    LevelManager.WaveNum = 1; 
                }
            }
                
        }

        public void AddEnemy(Ship type)
        {
            type.OnDeath = DeathAction;
            waveCount++;
            World.Instance.AddObject(type);
        }
    }
}
