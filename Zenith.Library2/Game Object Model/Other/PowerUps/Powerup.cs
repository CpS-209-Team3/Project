//-----------------------------------------------------------
//File:   PowerUp.cs
//Desc:   Holds the abstract class used for all power ups.
//Note:   This file is unused.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    // Acts as the basis for all power ups.
    // It ameliorates a single, or multiple, traits of a single ship
    // upon collision, whether it be laser damage, reload time, or
    // health.
    public abstract class PowerUp : GameObject
    {
        // Sets the velocity to a constant value so that
        // it is always moving closer towards the player.
        public override void Loop()
        {
            velocity.X = -1;
        }

        // Checks if it collides with a ship (even an enemy ship!),
        // and applies the power up properties to that ship. It then
        // will destroy itselt so that it is only used once.
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

        // Constructor
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
