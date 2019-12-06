using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    public abstract class Ship : GameObject
    {

        const double collisionDamper = 0.50;

        // instance variables

        protected int reloadTime = 0;
        protected int fireRate = 15;
        protected int bodyDamage = 100;

        protected double direction = 0;
        protected double accuracy = 0.05;
        protected int laserDamage = 40;
        protected double laserSpeed = 400;

        protected int health = 120;
        protected int maxHealth = 120;

        private Vector shakeOffset;
        private int shakeTime = 0;
        private int shakeDuration = 30;

        private Action onDeath;

        protected Cannon cannon;

        // Properties

        public int Health { get { return health; } set { health = value; } }
        public int BodyDamage { get { return bodyDamage; } set { bodyDamage = value; } }
        public Vector ShakeOffSet { get { return shakeOffset; } }
        public Action OnDeath { get { return onDeath; } set { onDeath = value; } }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

        // Methods

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

        protected void Shake()
        {
            shakeTime = shakeDuration;
        }

        public void MoveTo(Vector destination, double acceleration)
        {
            var f = (destination - position) - (velocity / 60);
            f.Magnitude = mass * acceleration;
            AddForce(f);
        }

        public override void Loop()
        {
            ShipLoop();
            if (health <= 0)
            {
                destroy = true;
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
            if (position.X > World.Instance.Width) AddForce(new Vector(-500, 0));
            velocity *= 0.97;

            cannon.Update();
        }

        public abstract void ShipLoop();

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

        public override string Serialize()
        {
            return base.Serialize() + ',' + reloadTime.ToString() + ',' + fireRate.ToString() + ',' +
                bodyDamage.ToString() + ',' + direction.ToString() + ',' + accuracy.ToString() + ',' +
                laserDamage.ToString() + ',' + laserSpeed.ToString() + ',' + health.ToString() + ',' +
                maxHealth.ToString() + ',' + shakeOffset.ToString() + ',' + shakeTime.ToString() + ',' +
                shakeDuration.ToString() + ',' /*+ onDeath.ToString() + ','*/ + cannon.ToString();
        }

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

            //onDeath = shipSaveInfo[12];

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
        }

    }
}
