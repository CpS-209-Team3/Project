using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    abstract class Ship : GameObject
    {
        protected int health;
        protected int reloadTime;
        protected int bodyDamage;
        protected int laserDamage;

        protected double accerlation;

        public override void Loop() { }

        public Ship(Vector position)
            : base(position)
        {

        }
    }
}
