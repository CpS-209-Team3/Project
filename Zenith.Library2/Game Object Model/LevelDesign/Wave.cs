using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave
    {
        protected int waveCount;
   
        public void DecreaseCount()
        {
            waveCount--;
            if (waveCount == 0) LevelManager.NextWave++;
        }
    }
}
