using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss2 : Enemy
    {
        public override void ShipLoop() {
            cannon.Fire();
            ++clock;
            double goalY = (Math.Cos((double)clock / 100) + 1) / 2 * World.Instance.Height - position.Y;
            AddForce(new Vector(0, goalY) * 100);
            var offset = new Vector(World.Instance.Width * 0.75 - position.X, 0);
            AddForce(offset);

            angle = (World.Instance.Player.Position - position).Angle;
        }

        public Boss2(Vector position)
            : base(position) 
        {
            type = GameObjectType.Boss2;
            cannon = new Boss2Cannon(this);
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("large_purple_01.png") };

            size = new Vector(256, 256);
            health = 4000;
            maxHealth = 4000;
            mass = 400;

        }
    }
}
