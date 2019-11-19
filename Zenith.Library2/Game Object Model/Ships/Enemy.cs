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

    class Enemy : Ship
    {
        private Ship player;
        private int pointValue;
        private int damage;

        public override void Loop() { }

        public Enemy(Vector position, Ship player)
            : base(position)
        {
            this.player = player;
        }

        
    }
}
