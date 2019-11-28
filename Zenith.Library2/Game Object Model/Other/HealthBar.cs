using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class HealthBar : GameObject
    {
        Ship host;
        double distance;

        public override void Loop()
        {
            distance = Math.Max(host.Size.X, host.Size.Y);
            position = host.Position - new Vector(0, distance);
            destroy = host.Destroy;
        }

        public HealthBar(Ship host)
            : base(host.Position)
        {
            this.host = host;
            imageSource = Util.GetSpriteFolderPath("Health_Bar\\healthbar_10.png");
            imageRotation = 0;
            collidable = false;
            type = GameObjectType.HealthBar;
            size *= 100;
        }
    }
}
