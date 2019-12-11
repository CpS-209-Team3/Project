using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public abstract class Wave
    {
        protected int difficulty;
        protected int level;
        protected int waveCount = 0;
        protected Vector startingPos;
        protected double size; 

        public int WaveCount { get { return waveCount; } set { waveCount = value; } }

        public List<Enemy> enemies;

        public void DeathAction()
        {
            waveCount--;
            if (waveCount == 0)
            {
                if (LevelManager.WaveNum < 5)
                {
                    World.Instance.CurrentWave++;
                }
                else
                {
                    World.Instance.Level++;
                    LevelManager.WaveNum = 1; 
                }
            }
                
        }

        public virtual void Spawn()
        {

        }
        public Wave()
        {
            this.difficulty = World.Instance.Difficulty;
            this.level = World.Instance.Level;
        }

        public void AddEnemy(Ship type)
        {
            type.OnDeath = DeathAction;
            waveCount++;
            World.Instance.AddObject(type);
        }
    }
}
