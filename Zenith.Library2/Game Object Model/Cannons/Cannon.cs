//-----------------------------------------------------------
//File:   Cannon.cs
//Desc:   Holds the main class for all cannons, a enum for
//        laser colors, and an interface for playing sounds.
//----------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public interface ISoundPlayer
    {
        void StartSoundEffect(String soundFilePath);
    }

    public enum ProjectileColor
    {
        Blue, Green, Orange, Red
    }

    // This class serves as the base class for all cannons
    // in Zenith. It provides methods for firing lasers
    // and handles different firing patterns and updating
    // reload times.
    public class Cannon
    {
        // instance variables

        // colors is used to control
        private static string[] colors = new string[] {
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-blue.png"),
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-green.png"),
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-orange.png"),
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-red.png")
        };

        // Interface for playing sounds
        protected ISoundPlayer soundPlayer;

        // The owner of the cannon.
        protected Ship host;

        // The amount of game ticks required before the cannon can spawn the next laser.
        protected int reloadTime = 0;

        // The reload time after each successive laser is fire.
        // Once the last value is reached, the list is looped back to the beginning.
        protected List<int> firePattern = new List<int> { 15 };

        // The index in firePattern that the cannon is currently at.
        protected int fireSequence = 0;

        // The damage of each laser.
        protected int damage = 40;

        // The angular difference, in radians, that a laser can be launched at in relation to the cannon's angle
        protected double accuracy = 0;

        // The speed at which a laser can be launched at.
        protected double projectileSpeed = 800;

        // The color at which to set initialized lasers.
        protected string projectileColor = colors[0];

        //  properties
        public Ship Host { get { return host; } set { host = value; } }
        public int ReloadTime { get { return reloadTime; } set { reloadTime = value; } }
        public List<int> FirePattern { get { return firePattern; } }
        public int FireSequence { get { return fireSequence; } set { fireSequence = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public double Accuracy { get { return accuracy; } set { accuracy = value; } }
        public double ProjectileSpeed { get { return projectileSpeed; } set { projectileSpeed = value; } }
        public ProjectileColor ProjectileColor { set { projectileColor = colors[(int)value]; } }

        // Methods

        // When called, reloadTime will be checked if it is less than 0.
        // If so, the initial direction, velocity, and position is calculated
        // and used to create a new laser instance. This laser is then added
        // to World's object list. The reloadTime and fireSequence variables
        // are then updated as necessary.
        public virtual void Fire()
        {
            if (reloadTime <= 0)
            {
                double aim = host.Angle + World.Instance.Random.NextDouble() * (accuracy * 2) - accuracy;
                var vel = new Vector(aim, projectileSpeed, true);
                var offset = new Vector(host.Angle, host.Size.X / 2, true);
                var laser = new Laser(host.Position + offset, vel, damage, host is Player);
                laser.ImageSources.Clear();
                laser.ImageSources.Add(projectileColor);
                World.Instance.AddObject(laser);

                reloadTime += firePattern[fireSequence];
                fireSequence = (fireSequence + 1) % firePattern.Count;

                // SoundPlayer.StartSoundEffect(SoundEffectLocation);
            }
        }

        // Decrements reloadTime if it is less than 0.
        public virtual void Update()
        {
            if (reloadTime > 0) --reloadTime;
        }

        // Constructor
        // Sets the host of the cannon.
        public Cannon(Ship host)
        {
            this.host = host;
        }

        // ~~~~~~~~~~~~~~~~~~~~ Change the default ToString method for serialize/deserialize ~~~~~~~~~~~~~~~~~~~~
        public override string ToString()
        {
            string firePatterns = "";
            foreach (int i in firePattern)
            {
                firePatterns += i.ToString() + ';';
            }
            firePatterns.Remove(firePatterns.Length - 1);
            return reloadTime.ToString() + ':' + firePatterns + ':' +
                fireSequence.ToString() + ':' + damage.ToString() + ':' + accuracy.ToString() + ':' + projectileSpeed.ToString();
        }
    }
}
