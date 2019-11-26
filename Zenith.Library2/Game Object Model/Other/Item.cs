using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Item : GameObject
    {

        public override void Loop() { }

        public Item(Vector position)
            : base(position)
        {

        }
    }
}
