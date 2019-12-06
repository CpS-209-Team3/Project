using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss5 : Enemy
    {
        Sensor sensor;
        Vector avoid = new Vector(0, 0);

        public override void ShipLoop()
        {
            if (avoid.Magnitude > 1)
            {
                AddForce(avoid);
                avoid /= 2;
            }
            else
            {
                MoveTo(new Vector(World.Instance.Width * 0.75, World.Instance.Height / 2), 10);
            }
        }

        public void OnSense(GameObject gameObject)
        {
            if (gameObject is Laser)
            {
                var laser = (Laser)gameObject;
                if (laser.IsFromPlayer)
                {
                    var offset = position - laser.Position;
                    double dist = offset.Magnitude;
                    offset.Magnitude = 6000000;
                    avoid =  offset / (dist * dist);
                }
            }
        }

        public Boss5(Vector position)
            : base(position)
        {
            type = GameObjectType.Boss5;
            imageSources = new List<string> { Util.GetShipSpriteFolderPath("large_green_01.png") };
            size = new Vector(256, 256);
            mass = 400;
            health = 4000;
            maxHealth = 4000;

            sensor = new Sensor(this, OnSense, 500);
        }
    }
}
