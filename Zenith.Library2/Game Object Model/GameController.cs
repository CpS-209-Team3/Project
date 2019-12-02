using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class GameController
    {
        bool up, down, left, right, fire, pause;

        public bool Up { get { return up; } set { up = value; } }
        public bool Down { get { return down; } set { down = value; } }
        public bool Left { get { return left; } set { left = value; } }
        public bool Right { get { return right; } set { right = value; } }
        public bool Fire { get { return fire; } set { fire = value; } }
        public bool Pause { get { return pause; } set { pause = value; } }
    }
}
