using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Laser : GameObject
    {
        private int damage;
        private Ship owner;

        public Ship Owner
        {
            get { return owner; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject != owner)
            {
                Destroy = true;
            }
        }

        public override void Loop() { }

        public Laser(Ship owner, Vector position, Vector velocity, int damage)
            : base(position)
        {
            this.owner = owner;
            this.velocity = velocity;
            this.damage = damage;
        }
    }
}
