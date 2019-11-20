using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Enemy1 : Enemy
    {
        public override void Loop() { }

        public Enemy1(Vector position)
           : base(position)
        {
            imageSource = Util.GetImagePath("blue_01.png");
        }
    }
}
