using System;
using System.Collections.Generic;
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
            if ((isFromPlayer && gameObject is Enemy) || (!isFromPlayer && !(gameObject is Enemy)))
            {
                Destroy = true;
            }
        }

        public override void Loop() { }

        public Laser(bool isFromPlayer, Vector position, Vector velocity, int damage)
            : base(position)
        {
            this.isFromPlayer = isFromPlayer;
            this.velocity = velocity;
            this.damage = damage;
        }

        public override string Serialize()
        {
            return base.Serialize() + ',' + isFromPlayer.ToString() + ',' + damage.ToString();
        }

        public override void Deserialize(string saveInfo)
        {
            base.Deserialize(saveInfo);
            
        }
    }
}
