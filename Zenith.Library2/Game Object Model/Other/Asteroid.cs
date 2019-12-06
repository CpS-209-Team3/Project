using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Asteroid : Enemy
    {
        public override void ShipLoop()
        {
        }

        public Asteroid(Vector position, double size)
            : base(position)
        {
            this.size = new Vector(size, size);
            mass = size * size;
            velocity.X = (-1 / size * 10) * 6000;
            velocity.Y = (World.Instance.Random.NextDouble() * 2 - 1) * 8;
            type = GameObjectType.Asteroid;
            imageSources = new List<string> { Util.GetSpriteFolderPath("Aster1.png") };
            worth = 20;
            bodyDamage = 20;
        }
    }
}
