using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Wave
    {
        protected int waveCount;

        public int WaveCount { get { return waveCount; } set { waveCount = value; } }

        public void DecreaseCount()
        {
            waveCount--;
            if (waveCount == 0) LevelManager.WaveNum++;
        }
    }
}
