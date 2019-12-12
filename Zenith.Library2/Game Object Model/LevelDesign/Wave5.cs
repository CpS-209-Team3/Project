using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave5 : Wave
    {
        public override void Spawn()
        {
            var boss = World.Instance.SpawnBoss(level);
            if (level == 5) boss.OnDeath = World.Instance.OnGameFinish;
        }
    }
}
