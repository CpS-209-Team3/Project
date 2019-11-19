using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Game_Object_Model
{
    class BackgroundElement : GameObject
    {
        public override void Loop() { }

        public BackgroundElement(Vector position, double speed)
            : base(position)
        {
            velocity.X = 0;
        }
    }
}
