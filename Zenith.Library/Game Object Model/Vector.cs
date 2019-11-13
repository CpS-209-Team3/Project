using System;
using System.Collections.Generic;
using System.Text;

namespace Zenith.Library
{
    public class Vector
    {
        double x, y;
        double angle, magnitude;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading
        public static Vector operator +(Vector v1, Vector v2) {
            return new Vector(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vector operator *(Vector v1, double x)
        {
            return new Vector(v1.x - x, v1.y * x);
        }

        public Vector(double x, double y, bool isPolar = false)
        {
            if (isPolar)
            {
                this.x = Math.Cos(x) * y;
                this.y = Math.Sin(x) * y;
                this.magnitude = y;
                this.angle = x;
            }
            else
            {
                this.x = x;
                this.y = y;
                this.angle = Math.Atan2(y, x);
                this.magnitude = Math.Sqrt(x * x + y * y);
            }
        }
    }
}
