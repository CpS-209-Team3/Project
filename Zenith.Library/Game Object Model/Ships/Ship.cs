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
        protected double accuracy;
        protected double laserSpeed;

        protected double accerlation;

        public virtual void Shoot(double angle)
        {
            var vel = new Vector(angle, laserSpeed, true);
            var laser = new Laser(position, vel, laserDamage);
            World.AddObject(laser);
        }

        public override void Loop() { }

        public Ship(Vector position)
            : base(position)
        {

        }
    }
}
