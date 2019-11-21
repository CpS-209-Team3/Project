using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    public abstract class Ship : GameObject
    {
        // instance variables
        protected bool isPlayer;

        protected int health;
        protected int reloadTime;
        protected int bodyDamage;

        protected int laserDamage;
        protected double accuracy;
        protected double laserSpeed;

        // Properties

        public bool IsPlayer { get { return isPlayer; } set { isPlayer = value; } }

        public int Health { get { return health; } set { health = value; } }
        public int ReloadTime { get { return reloadTime; } set { reloadTime = value; } }
        public int BodyDamage { get { return bodyDamage; } set { bodyDamage = value; } }
        
        public int LaserDamage { get { return laserDamage; } set { laserDamage = value; } }
        public double Accuracy { get { return accuracy; } set { accuracy = value; } }
        public double LaserSpeed { get { return laserSpeed; } set { laserSpeed = value; } }
        
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

        public void Shoot(double angle)
        {
            var vel = new Vector(angle, laserSpeed, true);
            var laser = new Laser(isPlayer, position, vel, laserDamage);
            World.Instance.AddObject(laser);
        }

        public override void Loop() { }

        public Ship(Vector position)
            : base(position)
        {
            type = GameObjectType.Ship;
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
