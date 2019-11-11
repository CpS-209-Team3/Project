using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library.Game_Object_Model
{
    class Vector
    {
        double x, y;
        double angle, magnitude;

        double X
        {
            get { return x; }
            set { x = value; }
        }
        double Y
        {
            get { return y; }
            set { y = value; }
        }

        public static Vector operator +(Vector v1, Vector v2) {
            return new Vector(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y);
        }

        public Vector(double arg1, double arg2, bool isPolar = false)
        {
            if (isPolar)
            {
                this.x = Math.Cos(arg1) * arg2;
                this.y = Math.Sin(arg1) * arg2;
                this.magnitude = arg2;
                this.angle = arg1;
            }
            else
            {
                this.x = arg1;
                this.y = arg2;
                this.angle = Math.Atan2(arg2, arg1);
                this.magnitude = Math.Sqrt(arg1 * arg1 + arg2 * arg2);
            }
        }
    }
}
