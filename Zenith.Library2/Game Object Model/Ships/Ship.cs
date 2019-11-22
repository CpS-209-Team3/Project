using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    public abstract class Ship : GameObject
    {
        // instance variables
        protected bool isPlayer = false;

        protected int health = 100;
        protected int reloadTime = 0;
        protected int fireRate = 15;
        protected int bodyDamage = 100;

        protected double direction = 0;
        protected double accuracy = 0.2;
        protected int laserDamage = 4000;
        protected double laserSpeed = 400;

        private Vector shakeOffset;
        private int shakeTime = 0;
        private int shakeDuration = 60;

        // Properties

        public bool IsPlayer { get { return isPlayer; } set { isPlayer = value; } }
        public int Health { get { return health; } set { health = value; } }
        public int ReloadTime { get { return reloadTime; } set { reloadTime = value; } }
        public int BodyDamage { get { return bodyDamage; } set { bodyDamage = value; } }
        public int LaserDamage { get { return laserDamage; } set { laserDamage = value; } }
        public double Accuracy { get { return accuracy; } set { accuracy = value; } }
        public double LaserSpeed { get { return laserSpeed; } set { laserSpeed = value; } }
        public Vector ShakeOffSet { get { return shakeOffset; } }

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
            if (health <= 0)
            {
                Destroy = true;
            }
        }

        public void Shoot()
        {
            if (reloadTime <= 0)
            {
                double aim = direction + World.Instance.Random.NextDouble() * (accuracy * 2) - accuracy;
                var vel = new Vector(aim, laserSpeed, true);
                var laser = new Laser(position, vel, laserDamage, isPlayer);
                World.Instance.AddObject(laser);
                reloadTime += fireRate;
            }
        }

        private void Shake()
        {
            shakeTime += shakeDuration;
        }

        public override void Loop()
        {
            ShipLoop();
            if (reloadTime > 0) reloadTime -= 1;
            if (position.X < 0) Destroy = true;
        }

        public abstract void ShipLoop();

        public Ship(Vector position)
            : base(position)
        {
            type = GameObjectType.Ship;
            size = new Vector(48, 48);
            shakeOffset = new Vector(0, 0);
            if (shakeTime > 0)
            {
                double x = (World.Instance.Random.NextDouble() * 2 - 1) * 4;
                double y = (World.Instance.Random.NextDouble() * 2 - 1) * 4;
                shakeOffset = new Vector(x, y);
                --shakeTime;
            }
            else
            {
                shakeOffset.Cap(0);
            }
        }

        public override string Serialize()
        {
            return base.Serialize() + ',' + isPlayer.ToString() + ',' + health.ToString() + ',' + reloadTime.ToString() + ',' + bodyDamage.ToString() + ',' + laserDamage.ToString() + ',' + accuracy.ToString() + ',' + laserSpeed.ToString();
        }

        public override void Deserialize(string saveInfo)
        {
            int i = 0;
            int index = IndexOfNthOccurance(saveInfo, ",", 5);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            string[] shipSaveInfo = saveInfo.Substring(index + 1, saveInfo.Length - index - 1).Split(',');
            base.Deserialize(gameObjectSaveInfo);

            isPlayer = Convert.ToBoolean(shipSaveInfo[0]);
            health = Convert.ToInt32(shipSaveInfo[1]);
            reloadTime = Convert.ToInt32(shipSaveInfo[2]);
            bodyDamage = Convert.ToInt32(shipSaveInfo[3]);
            laserDamage = Convert.ToInt32(shipSaveInfo[4]);
            accuracy = Convert.ToDouble(shipSaveInfo[5]);
            laserSpeed = Convert.ToDouble(shipSaveInfo[6]);
        }

    }
}
