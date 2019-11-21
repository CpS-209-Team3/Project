using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Player : Ship
    {
        private double acceleration = 0.0001;

        public override void Loop()
        {
            if (World.Instance.PlayerController.Up) this.velocity += new Vector(0, -acceleration);
            if (World.Instance.PlayerController.Down) this.velocity += new Vector(0, acceleration);
            if (World.Instance.PlayerController.Left) this.velocity += new Vector(-acceleration, 0);
            if (World.Instance.PlayerController.Right) this.velocity += new Vector(acceleration, 0);
        }

        public Player(Vector position)
            : base(position)
        {
            imageSource = Util.GetImagePath("blue_01.png");
        }
    }
}
