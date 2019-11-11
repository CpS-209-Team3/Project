using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Laser : GameObject
    {
        private int damage;

        public override void Loop() { }

        public Laser(Vector position, Vector velocity, int damage)
            : base(position)
        {
            this.velocity = velocity;
            this.damage = damage;
        }
    }
}
