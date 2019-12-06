using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public abstract class Wave
    {
        protected int waveCount = 0;
        protected Vector startingPos = new Vector(World.Instance.Width, World.Instance.Random.Next(0, Convert.ToInt32(World.Instance.Height)), false);
        protected double size = World.Instance.Random.NextDouble() * 100 + 30;
        public int WaveCount { get { return waveCount; } set { waveCount = value; } }

        public void DecreaseCount()
        {
            waveCount--;
            if (waveCount == 0) LevelManager.WaveNum++;
        }

        public void AddEnemy(Enemy type)
        {
            type.OnDeath = DecreaseCount;
            World.Instance.AddObject(type);
            waveCount++;
        }
    }
}
