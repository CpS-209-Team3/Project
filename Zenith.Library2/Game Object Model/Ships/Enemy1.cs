using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Enemy1 : Enemy
    {
        public override void ShipLoop() {
            
            cannon.Fire();
            MoveTo(World.Instance.Player.Position);
        }

        public Enemy1(Vector position)
           : base(position)
        {
            type = GameObjectType.Enemy1;

            imageSources = new List<string> { Util.GetShipSpriteFolderPath("blue_01.png") };
            angle = Math.PI;
            velocity.X = -500 + 100 * 1;
            cannon = new BasicCannon(this, 500);
            swayRadius = 0;
            maxHealth = 50;
            health = 50;

        }
    }
}
