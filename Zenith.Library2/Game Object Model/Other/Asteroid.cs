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
            velocity.X = World.Instance.Random.NextDouble() * 2 - 1;
            velocity.Y = World.Instance.Random.NextDouble() * 2 - 1;
        }

        public string Serialize()
        {
            return "Asteroid," + position.ToString() + "," + size.ToString() + "," + velocity.ToString();
        }

        public void Deserialize(string saveInfo)
        {
            string[] savedValues = saveInfo.Split(',');
            string[] xNy0 = savedValues[0].Split(':');
            position = new Vector(Convert.ToDouble(xNy0[0]), Convert.ToDouble(xNy0[1]), false);
            string[] xNy1 = savedValues[1].Split(':');
            position = new Vector(Convert.ToDouble(xNy1[0]), Convert.ToDouble(xNy1[1]), false);
            string[] xNy2 = savedValues[2].Split(':');
            position = new Vector(Convert.ToDouble(xNy2[0]), Convert.ToDouble(xNy2[1]), false);
        }
    }
}
