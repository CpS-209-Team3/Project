using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public interface ISoundPlayer
    {
        void StartSoundEffect( String soundFilePath);
    }
    public enum ProjectileColor
    {
        Blue, Green, Orange, Red 
    }
    public class Cannon
    {
        // instance variables

        private static string[] colors = new string[] {
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-blue.png"),
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-green.png"),
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-orange.png"),
            Util.GetShipSpriteFolderPath("Projectiles\\projectile-red.png")
        };

        protected ISoundPlayer soundPlayer;
        protected Ship host;
        protected int reloadTime = 0;
        protected List<int> firePattern = new List<int> { 15 };
        protected int fireSequence = 0;
        protected int damage = 40;
        protected double accuracy = 0;
        protected double projectileSpeed = 800;
        protected string projectileColor = colors[0];
        protected string soundEffectLocation;

        //  properties
        public ISoundPlayer SoundPlayer { get { return soundPlayer; } set { soundPlayer = value; } }
        public Ship Host { get { return host; } set { host = value; } }
        public int ReloadTime { get { return reloadTime; } set { reloadTime = value; } }
        public List<int> FirePattern { get { return firePattern; } }
        public int FireSequence { get { return fireSequence; } set { fireSequence = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public double Accuracy { get { return accuracy; } set { accuracy = value; } }
        public double ProjectileSpeed { get { return projectileSpeed; } set { projectileSpeed = value; } }
        public ProjectileColor ProjectileColor { set { projectileColor = colors[(int)value]; } }
        public String SoundEffectLocation { get { return soundEffectLocation; } set { soundEffectLocation = value; } }

        // Methods

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

        public virtual void Update()
        {
            if (reloadTime > 0) --reloadTime;
        }

        public Cannon(Ship host)
        {
            this.host = host;
        }

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
