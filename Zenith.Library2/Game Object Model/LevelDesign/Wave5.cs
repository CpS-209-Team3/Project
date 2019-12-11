using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Wave5 : Wave
    {
        public Wave5(int difficulty, int level)
        {
            var boss = World.Instance.SpawnBoss(level);
            if (level == 5) boss.OnDeath = World.Instance.OnGameFinish;
            AddEnemy(boss);
        }
    }
}
