using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class GameController
    {
        bool up, down, left, right, a, b, c, d;

        public bool Up { get { return up; } set { up = value; } }
        public bool Down { get { return down; } set { down = value; } }
        public bool Left { get { return left; } set { left = value; } }
        public bool Right { get { return right; } set { right = value; } }
        public bool A { get { return a; } set { a = value; } }
        public bool B { get { return b; } set { b = value; } }
        public bool C { get { return c; } set { c = value; } }
        public bool D { get { return d; } set { d = value; } }
    }
}
