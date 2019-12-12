//-----------------------------------------------------------
//File:   Ship.cs
//Desc:   Defines the abstract Ship class.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    // This class controls all Enemies, Bosses, Players, and Asteroids.
    // It also defines some common methods used all all as well.
    public abstract class Ship : GameObject
    {
        // The percentage to decrease the force applied after colliding by.
        const double collisionDamper = 0.50;

        // instance variables


        protected int reloadTime = 0;
        protected int fireRate = 15;
        protected int bodyDamage = 100;

        protected double direction = 0;
        protected double accuracy = 0.05;
        protected int laserDamage = 40;
        protected double laserSpeed = 400;

        // The current amount of health the ship
        // has.
        protected int health = 120;

        // The maximum possible amount of health
        // a ship can have.
        protected int maxHealth = 120;

        // The change in position of a ship to make
        // the shake happen.
        private Vector shakeOffset;

        // The game ticks left until a shake is over.
        private int shakeTime = 0;

        // The amount of game ticks that a shake should
        // last for.
        private int shakeDuration = 30;

        // The callback method that is called whenever the
        // ship is destroyed.
        protected Action onDeath;

        // The cannon that will be used by the player.
        protected Cannon cannon;

        // The amount of points added to the player's score after it is destroyed.
        protected int worth;
        
        // Properties

        public int Health { get { return health; } set { health = value; } }
        public int BodyDamage { get { return bodyDamage; } set { bodyDamage = value; } }
        public Vector ShakeOffSet { get { return shakeOffset; } }
        public Action OnDeath { get { return onDeath; } set { onDeath = value; } }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
        public int Worth { get { return worth; } }

        // Methods

        // This collision check only will respond to 2 kinds of objects:
        // Ship:
        //      The current ship object will Shake and will bounce
        //      off the other ship. If one of the ships is a Player,
        //      then this.health will be decremented by the other
        //      ship's body damage.
        // Laser:
        //      If this ship is a Player and the laser is not from
        //      a player, or vice versa, then decrement health by
        //      the laser's Damage value.
        public override void OnCollision(GameObject gameObject)
        {
            var offset = (position - gameObject.Position);
            switch (gameObject.Tag)
            {
                case GameTag.Ship:
                    if ((this is Player) != (gameObject is Player))
                    {
                        var ship = (Ship)gameObject;
                        health -= ship.BodyDamage;
                        Shake();
                    }
                    AddForce(offset * ((gameObject.Velocity.Magnitude) * gameObject.Mass / mass) * collisionDamper);
                    break;
                case GameTag.Projectile:
                    var laser = (Laser)gameObject;

                    if (laser.IsFromPlayer != this is Player)
                    {
                        health -= laser.Damage;
                        AddForce(offset * (gameObject.Velocity.Magnitude * gameObject.Mass / mass) * collisionDamper);
                        Shake();
                    }
                    break;
            }
        }

        // Increment shakeTime to make the Ship
        // Shake.
        protected void Shake()
        {
            shakeTime = shakeDuration;
        }

        // Moves the ship towards a specified position given
        // an acceleration.
        public void MoveTo(Vector destination, double acceleration)
        {
            var f = (destination - position) - (velocity / 60);
            f.Magnitude = mass * acceleration;
            AddForce(f);
        }

        // Calls ShipLoop(). Also checks if the ship's health is
        // less than or equal to 0 and deals with that accordingly.
        // This method also handles the shaking of all ships, updates
        // the cannon, and continuously slows down all ships.
        public override void Loop()
        {
            ShipLoop();
            if (health <= 0)
            {
                destroy = true;
                World.Instance.ViewManager.PlaySound("Explode");

                if (World.Instance.Player.Health > 0)
                {
                    World.Instance.Score += worth;
                }
                
                onDeath?.Invoke();
                return;
            }
            if (shakeTime > 0)
            {
                position -= shakeOffset;

                double x = (World.Instance.Random.NextDouble() * 2 - 1) * 4;
                double y = (World.Instance.Random.NextDouble() * 2 - 1) * 4;
                shakeOffset = new Vector(x, y);
                --shakeTime;

                position += shakeOffset;
            }
            else
            {
                shakeOffset.Cap(0);
            }
            if (position.X > World.Instance.EndX) AddForce(new Vector(-500, 0));
            velocity *= 0.97;

            cannon.Update();
        }

        // A method specifically called to update
        // all ships.
        public abstract void ShipLoop();

        // Applys a powerup's properties to the ship.
        public void ApplyPowerUp(PowerUp power)
        {
            cannon.Damage += power.Damage;
            if (power.Health) health = maxHealth;
            for (int i = 0; i < cannon.FirePattern.Count; ++i)
            {
                if (cannon.FirePattern[i] > 0) cannon.FirePattern[i] -= 1;
            }

            size *= 1.20;
        }

        // Constructor
        public Ship(Vector position)
            : base(position)
        {
            type = GameObjectType.Ship;
            size = new Vector(48, 48);
            shakeOffset = new Vector(0, 0);
            mass = 50;
            tag = GameTag.Ship;

            cannon = new Cannon(this);

            var h = new HealthBar(this);
            World.Instance.AddObject(h);
        }

        // This method turns all the necessary Ship variables into strings and turns them into a line of
        // comma seperated values so that they can be loaded in later.
        public override string Serialize()
        {
            return base.Serialize() + ',' + reloadTime.ToString() + ',' + fireRate.ToString() + ',' +
                bodyDamage.ToString() + ',' + direction.ToString() + ',' + accuracy.ToString() + ',' +
                laserDamage.ToString() + ',' + laserSpeed.ToString() + ',' + health.ToString() + ',' +
                maxHealth.ToString() + ',' + shakeOffset.ToString() + ',' + shakeTime.ToString() + ',' +
                shakeDuration.ToString() + ',' + cannon.ToString() + ',' + worth.ToString();
        }
        // This method loads in all the necessary variables for a ship from a line of comma seperated values.
        public override void Deserialize(string saveInfo)
        {

            int index = IndexOfNthOccurance(saveInfo, ",", 11);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            base.Deserialize(gameObjectSaveInfo);

            string[] shipSaveInfo = saveInfo.Substring(index + 1, saveInfo.Length - index - 1).Split(',');

            reloadTime = Convert.ToInt32(shipSaveInfo[0]);
            fireRate = Convert.ToInt32(shipSaveInfo[1]);
            bodyDamage = Convert.ToInt32(shipSaveInfo[2]);

            direction = Convert.ToDouble(shipSaveInfo[3]);
            accuracy = Convert.ToDouble(shipSaveInfo[4]);
            laserDamage = Convert.ToInt32(shipSaveInfo[5]);
            laserSpeed = Convert.ToDouble(shipSaveInfo[6]);

            health = Convert.ToInt32(shipSaveInfo[7]);
            maxHealth = Convert.ToInt32(shipSaveInfo[8]);

            string[] xNy = shipSaveInfo[9].Split(':');
            shakeOffset = new Vector(Convert.ToDouble(xNy[0]), Convert.ToDouble(xNy[1]), false);
            shakeTime = Convert.ToInt32(shipSaveInfo[10]);
            shakeDuration = Convert.ToInt32(shipSaveInfo[11]);


            string[] cannonValues = shipSaveInfo[12].Split(':');

            cannon.Host = this;
            cannon.ReloadTime = Convert.ToInt32(cannonValues[0]);
            char[] charSeparators = new char[] { ';' };
            string[] firePatternValues = cannonValues[1].Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string value in firePatternValues)
            {
                cannon.FirePattern.Add(Convert.ToInt32(value));
            }
            cannon.FireSequence = Convert.ToInt32(cannonValues[2]);
            cannon.Damage = Convert.ToInt32(cannonValues[3]);
            cannon.Accuracy = Convert.ToDouble(cannonValues[4]);
            cannon.ProjectileSpeed = Convert.ToDouble(cannonValues[5]);
            worth = Convert.ToInt32(shipSaveInfo[13]);
        }

    }
}
