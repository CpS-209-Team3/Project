using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Cannon
    {
        GameObject host;
        int reloadTime = 0;
        protected int[] firePattern = { 15 };
        protected int fireSequence = 0;
        protected int damage = 40;
        protected double accuracy = 0;
        protected double projectileSpeed = 800;

        public virtual void Fire()
        {
            if (reloadTime <= 0)
            {
                double aim = host.Angle + World.Instance.Random.NextDouble() * (accuracy * 2) - accuracy;
                var vel = new Vector(aim, projectileSpeed, true);
                var offset = new Vector(host.Angle, host.Size.X / 2, true);
                var laser = new Laser(host.Position + offset, vel, damage, host is Player);
                World.Instance.AddObject(laser);

                reloadTime += firePattern[fireSequence];
                fireSequence = (fireSequence + 1) % firePattern.Length;
            }
        }

        public virtual void Update()
        {
            if (reloadTime > 0) --reloadTime;
        }

        public Cannon(GameObject host)
        {
            this.host = host;
        }

    }


}
