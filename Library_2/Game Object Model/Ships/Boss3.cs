using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    class Boss3 : Enemy
    {
        public override void Loop() { }

        public Boss3(Vector position)
            : base(position, null) { }
    }
}
