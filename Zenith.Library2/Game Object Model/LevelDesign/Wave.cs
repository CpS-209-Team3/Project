//-----------------------------------------------------------
//File:   Wave.cs
//Desc:   This abstract class defines all the necessary methods
//        and variables in order for the Waves to progress and
//        Spawn enemies based on the level and difficulty.
//----------------------------------------------------------- 



using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // This class provides methods
    public abstract class Wave
    {
        protected int difficulty;
        protected int level;
        protected int waveCount = 0;
        protected Vector startingPos;
        protected double size;

        public int WaveCount { get { return waveCount; } set { waveCount = value; } }

        public virtual void Spawn() { }
        
        public Wave()
        {
            this.difficulty = World.Instance.Difficulty;
            this.level = World.Instance.Level;
        }

        public void AddEnemy(Ship type)
        {
            type.OnDeath = World.Instance.DeathAction;
            waveCount++;
            World.Instance.AddObject(type);
        }
    }
}
