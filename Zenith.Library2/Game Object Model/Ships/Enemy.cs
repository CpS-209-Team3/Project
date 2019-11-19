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
        private int pointValue;
        private int damage;

        public override void Loop() { }

        public Enemy(Vector position)
            : base(position)
        {

        }
     }
}
