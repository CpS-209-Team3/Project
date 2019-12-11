using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave5 : Wave
    {
        public override void Spawn()
        {
            AddEnemy(World.Instance.SpawnBoss(level));
        }
    }
}
