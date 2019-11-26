using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    public abstract class Ship : GameObject
    {
        // instance variables
        protected int health = 200;
        protected int reloadTime = 0;
        protected int fireRate = 15;
        protected int bodyDamage = 100;

        protected double direction = 0;
        protected double accuracy = 0.05;
        protected int laserDamage = 40;
        protected double laserSpeed = 400;

        private Vector shakeOffset;
        private int shakeTime = 0;
        private int shakeDuration = 30;

        // Properties

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
                    if ((laser.IsFromPlayer && type != GameObjectType.Player) || (!laser.IsFromPlayer && type == GameObjectType.Player))
                    {
                        health -= laser.Damage;
                        Shake();
                    }
                    break;
                case GameObjectType.Ship:
                    var ship = (Ship)gameObject;
                    health -= ship.BodyDamage;
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
                var laser = new Laser(position, vel, laserDamage, type);
                World.Instance.AddObject(laser);
                reloadTime += fireRate;
            }
        }

        private void Shake()
        {
            shakeTime = shakeDuration;
        }

        public override void Loop()
        {
            ShipLoop();
            if (reloadTime > 0) reloadTime -= 1;
            if (position.X < 0) Destroy = true;
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
        }

        public abstract void ShipLoop();

        public Ship(Vector position)
            : base(position)
        {
            type = GameObjectType.Ship;
            size = new Vector(48, 48);
            shakeOffset = new Vector(0, 0);
        }

        public override string Serialize()
        {
            string stringifiedShipVariables = "";
            foreach (PropertyInfo property in typeof(Ship).GetProperties())
            {
                stringifiedShipVariables += ',' + property.ToString();
            }

            return base.Serialize() + stringifiedShipVariables;
        }

        public override void Deserialize(string saveInfo)
        {
            int index = IndexOfNthOccurance(saveInfo, ",", 5);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            string[] shipSaveInfo = saveInfo.Substring(index + 1, saveInfo.Length - index - 1).Split(',');
            base.Deserialize(gameObjectSaveInfo);

            health = Convert.ToInt32(shipSaveInfo[0]);
            reloadTime = Convert.ToInt32(shipSaveInfo[1]);
            fireRate = Convert.ToInt32(shipSaveInfo[2]);
            bodyDamage = Convert.ToInt32(shipSaveInfo[3]);

            direction = Convert.ToInt32(shipSaveInfo[4]);
            accuracy = Convert.ToDouble(shipSaveInfo[5]);
            laserDamage = Convert.ToInt32(shipSaveInfo[6]);
            laserSpeed = Convert.ToDouble(shipSaveInfo[7]);

            string[] xNy = shipSaveInfo[8].Split(':');
            shakeOffset = new Vector(Convert.ToDouble(xNy[0]), Convert.ToDouble(xNy[1]), false);
            shakeTime = Convert.ToInt32(shipSaveInfo[9]);
            shakeDuration = Convert.ToInt32(shipSaveInfo[10]);

        }

    }
}
