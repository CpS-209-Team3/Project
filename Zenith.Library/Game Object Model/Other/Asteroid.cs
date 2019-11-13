using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Asteroid : Enemy
    {
        public override void Loop() { }

        public Asteroid(Vector position, double size)
            : base(position, null)
        {
            this.size = new Vector(size, size);
            velocity.X = World.Random.NextDouble() * 2 - 1;
            velocity.Y = World.Random.NextDouble() * 2 - 1;
        }
    }
}
