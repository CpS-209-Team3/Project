using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    class Laser : GameObject
    {
        private int damage;
        private bool isFromPlayer;

        public bool IsFromPlayer
        {
            get { return isFromPlayer; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public override void OnCollision(GameObject gameObject)
        {
            // only does damage to the opponenet
            if (gameObject.Tag == GameTag.Ship)
            {
                // Found here that C# has a xor operator
                // Source: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators
                if (isFromPlayer ^ (gameObject is Player))
                {
                    Destroy = true;
                }
            }
        }

        public override void Loop()
        {
            if (position.X < 0 ||
                position.Y < 0 ||
                position.X > World.Instance.Width ||
                position.Y > World.Instance.Height) Destroy = true;
        }


        public Laser(Vector position, Vector velocity, int damage, bool isFromPlayer)

            : base(position)
        {
            this.isFromPlayer = isFromPlayer;
            this.velocity = velocity;
            this.damage = damage;
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("Projectiles\\projectile-blue.png") };


            imageRotation = 0;

            int size2 = Math.Min(damage, 100);
            size = new Vector(size2, size2);
            angle = velocity.Angle;

            type = GameObjectType.Laser;
            tag = GameTag.Projectile;
        }

        public override string Serialize()
        {
            return base.Serialize() + ',' + damage.ToString() + ',' + isFromPlayer.ToString();
        }

        public override void Deserialize(string saveInfo)
        {
            int index = IndexOfNthOccurance(saveInfo, ",", 11);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            base.Deserialize(gameObjectSaveInfo);

            string[] laserSaveInfo = saveInfo.Substring(index + 1, saveInfo.Length - index - 1).Split(',');
            damage = Convert.ToInt32(laserSaveInfo[0]);
            isFromPlayer = Convert.ToBoolean(laserSaveInfo[1]);
        }
    }
}
