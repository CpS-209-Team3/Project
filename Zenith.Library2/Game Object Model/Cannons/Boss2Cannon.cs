using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Boss2Cannon : Cannon
    {
        public override void Update()
        {
            if (reloadTime > 0)
            {
                double healthLeft = (double)Host.Health / Host.MaxHealth;
                int miniShot = (int)Math.Max(15 * healthLeft, 5);
                int gapTime = (int)Math.Max(100 * healthLeft, 60);

                damage = 200 - (int)(50 * healthLeft);

                FirePattern[0] = miniShot;
                FirePattern[1] = miniShot;
                FirePattern[2] = gapTime;
                --reloadTime;
            }
        }

        public Boss2Cannon(Ship host)
            : base(host)
        {
            firePattern = new List<int> { 15, 15, 100 };
            damage = 200;
            ProjectileColor = ProjectileColor.Red;
        }
    }
}
