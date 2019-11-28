using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss1 : Enemy
    {
        // Source: https://stackoverflow.com/questions/5142349/declare-a-const-array/5142378
        

        public override void ShipLoop()
        {
            Shoot();
            switch (state)
            {
                case EnemyState.Sway:
                    
                    
                    break;
            }
            ++clock;
            double goalY = (Math.Cos((double)clock / 100) + 1) / 2 * World.Instance.Height - position.Y;
            AddForce(new Vector(0, goalY));
            var offset = new Vector(World.Instance.Width * 0.75 - position.X, 0);
            AddForce(offset);

            angle = (World.Instance.Player.Position - position).Angle;
        }

        public Boss1(Vector position)
            : base(position)
        {
            firePattern = new int[] { 15, 15, 15, 100 };
            imageSources = new string[] { Util.GetShipSpriteFolderPath("large_grey_01.png") };
            angle = Math.PI;
            type = GameObjectType.Boss1;
            size = new Vector(256, 256);
            health = 4000;
            maxHealth = 4000;
            mass = 400;
            laserDamage = 100;
        }
    }
}
