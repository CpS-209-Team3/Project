using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public abstract class PowerUp : GameObject
    {
        public override void Loop()
        {
            velocity.X = -1;
        }

        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject is Ship)
            {
                var ship = (Ship)gameObject;
                ship.ApplyPowerUp(this);
                destroy = true;
            }
        }

        public int Damage { set; get; }
        public int ReloadTime { set; get; }
        public bool Health { set; get; }
        public int Duration { set; get; }

        public PowerUp(Vector position)
            : base (position)
        {
            imageSources = new List<string>() { Util.GetShipSpriteFolderPath("") };
            Damage = 0;
            ReloadTime = 0;
            Health = true;
            Duration = 300;
        }
    }
}
