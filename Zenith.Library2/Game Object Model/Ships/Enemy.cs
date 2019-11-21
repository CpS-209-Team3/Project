using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public enum EnemyState
    {
        Sway,
        Ram,
        Flee
    }

    public class Enemy : Ship
    {
        public override void Loop() { }

        public Enemy(Vector position)
            : base(position)
        {
            this.isPlayer = false;
            type = GameObjectType.Enemy;
        }

    }
}
