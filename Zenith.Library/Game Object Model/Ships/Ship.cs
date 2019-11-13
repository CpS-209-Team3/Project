using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    abstract class Ship : GameObject
    {
        // instance variables

        protected int health;
        protected int reloadTime;
        protected int bodyDamage;

        protected int laserDamage;
        protected double accuracy;
        protected double laserSpeed;

        protected double accerlation;

        // Properties

        public int BodyDamage { get { return bodyDamage; } }

        // Methods

        public override void OnCollision(GameObject gameObject)
        {
            var type = gameObject.Type;
            switch (type)
            {
                case GameObjectType.Laser:
                    var laser = (Laser)gameObject;
                    this.health -= laser.Damage;
                    break;
                case GameObjectType.Ship:
                    var ship = (Ship)gameObject;
                    this.health -= ship.BodyDamage;
                    break;
            }
        }

        public virtual void Shoot(double angle)
        {
            var vel = new Vector(angle, laserSpeed, true);
            var laser = new Laser(this, position, vel, laserDamage);
            World.Instance.AddObject(laser);
        }

        public override void Loop() { }

        public Ship(Vector position)
            : base(position)
        {

        }
    }
}
