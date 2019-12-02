using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Zenith.Library
{
    class Laser : GameObject
    {
        private int damage;
        private GameObjectType senderType;

        public GameObjectType SenderType
        {
            get { return senderType; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public override void OnCollision(GameObject gameObject)
        {
            // only does damage to the opponenet
            if (gameObject is Ship)
            {
                if (senderType != (gameObject.Type))
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


        public Laser(Vector position, Vector velocity, int damage, GameObjectType isFromPlayer)
            : base(position)
        {
            this.senderType = isFromPlayer;
            this.velocity = velocity;
            this.damage = damage;
            imageSource = Util.GetShipSpriteFolderPath("Projectiles\\projectile-blue.png");
            type = GameObjectType.Laser;
            size = new Vector(32, 32);
            imageRotation = velocity.Angle * 180 / Math.PI;
        }

        public override string Serialize()
        {
            return base.Serialize() + ',' + senderType.ToString() + ',' + damage.ToString();
        }

        public override void Deserialize(string saveInfo)
        {
            int i = 0;
            int index = IndexOfNthOccurance(saveInfo, ",", 5);

            string gameObjectSaveInfo = saveInfo.Substring(0, index);
            string[] laserSaveInfo = saveInfo.Substring(index, saveInfo.Length - index).Split(',');

            base.Deserialize(gameObjectSaveInfo);

            foreach (PropertyInfo property in typeof(Ship).GetProperties())
            {
                property.SetValue(this, laserSaveInfo[i]);
                i++;
            }
        }
    }
}
