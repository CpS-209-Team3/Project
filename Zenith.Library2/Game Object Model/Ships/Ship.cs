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

        }

        public override string Serialize()
        {
            string info = "";
            foreach (PropertyInfo property in typeof(Ship).GetProperties())
            {
                info += ',' + property.ToString();
            }
            return base.Serialize() + info;
        }

        public override void Deserialize(string saveInfo)
        {
            int i = 0;
            int index = IndexOfNthOccurance(saveInfo, ",", 5);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            string[] shipSaveInfo = saveInfo.Substring(index, saveInfo.Length - index).Split(',');

            base.Deserialize(gameObjectSaveInfo);

            foreach (PropertyInfo property in typeof(Ship).GetProperties())
            {
                property.SetValue(this, shipSaveInfo[i]);
                i++;
            }
        }

        // Got this method from here: https://stackoverflow.com/questions/186653/get-the-index-of-the-nth-occurrence-of-a-string
        // Returns the index of the nth occurance of a match within a string.
        
    }
}
