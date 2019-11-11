using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Asteroid : GameObject
    {
        double size;

        public override void Loop() { }

        public Asteroid(Vector position, double size)
            : base(position)
        {
            this.size = size;
        }
    }
}
