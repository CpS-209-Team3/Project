using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Asteroid : Enemy
    {
        public override void ShipLoop()
        {
            velocity.X = -1/size.X * 500;
        }

        public Asteroid(Vector position, double size)
            : base(position)
        {
            this.size = new Vector(size, size);
            mass = size * size;
            velocity.X = -World.Instance.Random.NextDouble() * 200;
            velocity.Y = (World.Instance.Random.NextDouble() * 2 - 1) * 8;
            type = GameObjectType.Asteroid;
            imageSources = new List<string> { Util.GetSpriteFolderPath("Aster1.png") };
        }
    }
}
