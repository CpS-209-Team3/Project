using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Asteroid : GameObject
    {
        public override void Loop() { }

        public Asteroid(Vector position, double size)
            : base(position)
        {
            this.size = new Vector(size, size);
            velocity.X = World.Random.NextDouble() * 2 - 1;
            velocity.Y = World.Random.NextDouble() * 2 - 1;
        }
    }
}
