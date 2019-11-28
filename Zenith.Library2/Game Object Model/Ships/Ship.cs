using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    public abstract class Ship : GameObject
    {
        // instance variables
        protected int health = 120;
        protected int maxHealth = 120;
        protected int bodyDamage = 0;

        private Vector shakeOffset;
        private int shakeTime = 0;
        private int shakeDuration = 30;

        private Action onDeath;

        protected Cannon cannon;

        // Properties

        public int Health { get { return health; } set { health = value; } }
        public int BodyDamage { get { return bodyDamage; } set { bodyDamage = value; } }
        public Vector ShakeOffSet { get { return shakeOffset; } }
        public Action OnDeath { set { onDeath = value; } }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

        // Methods

        public override void OnCollision(GameObject gameObject)
        {
            var offset = (position - gameObject.Position);
            switch (gameObject.Tag)
            {
                case GameTag.Ship:
                    if (gameObject.Type == GameObjectType.Player)
                    {
                        var ship = (Ship)gameObject;
                        health -= ship.BodyDamage;
                        Shake();
                    }
                    AddForce(offset * (gameObject.Velocity.Magnitude * gameObject.Mass / mass));
                    break;
                case GameTag.Projectile:
                    var laser = (Laser)gameObject;
                    if (laser.IsFromPlayer != this is Player)
                    {
                        health -= laser.Damage;
                        AddForce(offset * (gameObject.Velocity.Magnitude * gameObject.Mass / mass));
                        Shake();
                    }
                    break;
            }
        }

        protected void Shake()
        {
            shakeTime = shakeDuration;
        }

        public void MoveTo(Vector destination)
        {
            var f = (destination - position);
            f.Magnitude = 500;
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
            //return base.Serialize() + ',' + isPlayer.ToString() + ',' + health.ToString() + ',' + reloadTime.ToString() + ',' + bodyDamage.ToString() + ',' + laserDamage.ToString() + ',' + accuracy.ToString() + ',' + laserSpeed.ToString();
            //return base.Serialize() + ',' + health.ToString() + ',' + reloadTime.ToString() + ',' + bodyDamage.ToString() + ',' + laserDamage.ToString() + ',' + accuracy.ToString() + ',' + laserSpeed.ToString();
            return base.Serialize() + ',' + health.ToString() + ',' + bodyDamage.ToString();
        }

        public override void Deserialize(string saveInfo)
        {
            int i = 0;
            int index = IndexOfNthOccurance(saveInfo, ",", 5);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            string[] shipSaveInfo = saveInfo.Substring(index + 1, saveInfo.Length - index - 1).Split(',');
            base.Deserialize(gameObjectSaveInfo);

            //isPlayer = Convert.ToBoolean(shipSaveInfo[0]);
            health = Convert.ToInt32(shipSaveInfo[0]);
            //reloadTime = Convert.ToInt32(shipSaveInfo[1]);
            bodyDamage = Convert.ToInt32(shipSaveInfo[2]);
            //laserDamage = Convert.ToInt32(shipSaveInfo[3]);
            //accuracy = Convert.ToDouble(shipSaveInfo[4]);
            //laserSpeed = Convert.ToDouble(shipSaveInfo[5]);
        }

    }
}
